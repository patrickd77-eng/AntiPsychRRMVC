#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AntiPsychRRMVC.Data;
using AntiPsychRRMVC2.Models;

namespace AntiPsychRRMVC.Controllers
{
    public class DrugsController : Controller
    {
        private readonly AntiPsychRRMVCContext _context;

        public DrugsController(AntiPsychRRMVCContext context)
        {
            _context = context;
        }

        // GET: Drugs
        public async Task<IActionResult> Index()
        {
            var drugs = await _context.Drug
                .Include(f => f.DrugFrequency)
                .Include(d => d.DrugMaxDose)
                .Include(r => r.DrugRoute)
                .AsNoTracking().OrderBy(o=>o.DrugName.Trim()).ToListAsync();

            return View(drugs);
        }

        // GET: Drugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Drug drug)
        {
            if (ModelState.IsValid)
            {
                //Ensure string values are trimmed.
                drug.DrugName.Trim();
                drug.DrugRoute.RouteName.Trim();
                drug.DrugFrequency.FrequencyDetails.Trim();

                _context.Add(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drug);
        }

        // GET: Drugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drug
                .Include(f => f.DrugFrequency)
                .Include(d => d.DrugMaxDose)
                .Include(r => r.DrugRoute)
                .Where(i => i.DrugId == id)
                .AsNoTracking().FirstOrDefaultAsync();


            if (drug == null)
            {
                return NotFound();
            }
            return View(drug);
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Drug drug)
        {
            if (id != drug.DrugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Get drug that needs updating
                    var drugToUpdate = await _context.Drug
                         .Include(f => f.DrugFrequency)
                        .Include(d => d.DrugMaxDose)
                        .Include(r => r.DrugRoute)
                        .FirstOrDefaultAsync(d => d.DrugId == id);
                    //Make changes
                    drugToUpdate.DrugName = drug.DrugName;
                    drugToUpdate.DrugFrequency.FrequencyDetails = drug.DrugFrequency.FrequencyDetails;
                    drugToUpdate.DrugMaxDose.MaximumDoseLimit = drug.DrugMaxDose.MaximumDoseLimit;
                    drugToUpdate.DrugRoute.RouteName = drug.DrugRoute.RouteName;
                    //Save changes
                    _context.Drug.Update(drugToUpdate);
                    await _context.SaveChangesAsync();
                    //Redirect
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //Log error
                    ModelState.AddModelError(ex.ToString(), "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Drugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drug
                .Include(f => f.DrugFrequency)
                .Include(d => d.DrugMaxDose)
                .Include(r => r.DrugRoute)
                .FirstOrDefaultAsync(m => m.DrugId == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drug = await _context.Drug.FindAsync(id);
            _context.Drug.Remove(drug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugExists(int id)
        {
            return _context.Drug.Any(e => e.DrugId == id);
        }
    }
}
