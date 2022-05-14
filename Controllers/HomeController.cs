using AntiPsychRRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using AntiPsychRRMVC.Data;
using AntiPsychRRMVC.Enums;

namespace AntiPsychRRMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AntiPsychRRMVCContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AntiPsychRRMVCContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDoseFrequencies(string route)
        {
            try
            {
                var injection = route.Trim().ToLower().Contains("injection");
                var oral = route.Trim().ToLower().Contains("oral");
                var inhaled = route.Trim().ToLower().Contains("inhale");
                var intramuscular = route.Trim().ToLower().Contains("im");

                if (injection)
                {
                    var frequencies = getWeeklyFrequencies();
                    return Json(frequencies);
                }
                else if (oral || inhaled || intramuscular)
                {
                    var frequencies = getNonWeeklyFrequencies();
                    return Json(frequencies);
                }
                else
                {
                    return BadRequest("The list cannot be loaded. " +
                        "The selected drug does not have a valid frequency." +
                        "The valid values are" +
                        "depot," +
                        "injection," +
                        "oral," +
                        "inhaled" +
                        "or im.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        private List<string> getNonWeeklyFrequencies()
        {
            List<string> nonWeeklyFrequencies = Enum
                          .GetNames(typeof(DoseFrequencies.NonWeeklyFrequencies))
                          .ToList();

            return nonWeeklyFrequencies;
        }
        [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Client, NoStore = false)]
        private List<string> getWeeklyFrequencies()
        {
            List<string> weeklyFrequencies = Enum
                        .GetNames(typeof(DoseFrequencies.WeeklyFrequencies))
                        .ToList();

            return weeklyFrequencies;
        }

        [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> GetDrugList()
        {
            try
            {
                var drugs = await _context.Drug
                   .Include(f => f.DrugFrequency)
                   .Include(d => d.DrugMaxDose)
                   .Include(r => r.DrugRoute)
                   .AsNoTracking().OrderBy(o => o.DrugName.Trim()).ToListAsync();

                return Json(drugs);



            }
            catch (Exception ex)
            {
                //Log error
                ModelState.AddModelError(ex.ToString(), "Error. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");

                return BadRequest(ex);
            }
        }
        [ResponseCache(Duration = 3, Location = ResponseCacheLocation.None, NoStore = false)]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessSelectedDrug(int id, decimal dose, int frequencyModifier, string route)
        {

            try
            {
                var drugMaxDose = await _context.Drug
                     .Where(i => i.DrugId == id)
                     .Select(d => d.DrugMaxDose.MaximumDoseLimit)
                     .FirstOrDefaultAsync();

                //Figure out if dose needs division or multiplication based on route
                var injection = route.Trim().ToLower().Contains("injection");
                var oral = route.Trim().ToLower().Contains("oral");
                var inhaled = route.Trim().ToLower().Contains("inhale");
                var intramuscular = route.Trim().ToLower().Contains("im");

                if (injection && frequencyModifier > 1)
                {
                    //Divison - something other than 'once weekly' selected.
                    dose /= frequencyModifier;
                }
                else if ((oral || inhaled || intramuscular) && frequencyModifier > 1)
                {
                    //Multiplication - something other than 'once daily' selected.
                    dose *= frequencyModifier;
                }

                var result = new
                {
                    drugMaxDose = drugMaxDose,
                    dose = dose,
                    doseUtilisation = decimal.Round((dose / drugMaxDose * 100), 2)
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                //Log error
                ModelState.AddModelError(ex.ToString(), "Error. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");

                return BadRequest(ex);
            }
        }

        [ResponseCache(Duration = 3, Location = ResponseCacheLocation.None, NoStore = false)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}