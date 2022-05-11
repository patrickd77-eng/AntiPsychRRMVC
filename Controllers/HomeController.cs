﻿using AntiPsychRRMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using AntiPsychRRMVC.Data;

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
        [ResponseCache(Duration = 3, Location = ResponseCacheLocation.Client, NoStore = true)]
        public async Task<IActionResult> GetDrugList()
        {
            try
            {
                var drugs = await _context.Drug
                   .Include(f => f.DrugFrequency)
                   .Include(d => d.DrugMaxDose)
                   .Include(r => r.DrugRoute)
                   .AsNoTracking().ToListAsync();

                return Json(drugs);

                

            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }
        [ResponseCache(Duration = 3, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessSelectedDrug(int id, decimal dose)
        {

            var drugMaxDose = await _context.Drug
                   .Where(i => i.DrugId == id)
                   .Select(d=>d.DrugMaxDose.MaximumDoseLimit)
                   .FirstOrDefaultAsync();

            var result = new
            {
                drugMaxDose = drugMaxDose,
                dose = dose,
                doseUtilisation = dose / drugMaxDose * 100
            };

            return Json(result); 
        }

        [ResponseCache(Duration = 3, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}