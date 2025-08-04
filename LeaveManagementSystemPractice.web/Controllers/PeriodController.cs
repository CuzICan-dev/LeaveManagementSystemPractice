using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystemPractice.web.Data;
using LeaveManagementSystemPractice.web.Data.Entities;
using LeaveManagementSystemPractice.web.Services.Periods;
using Microsoft.AspNetCore.Authorization;

namespace LeaveManagementSystemPractice.web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class PeriodController(IPeriodService periodService) : Controller
    {
        private const string NameExistsValidationMessage = "Name already exists in the database";
        // GET: Period
        public async Task<IActionResult> Index()
        {
            var periods = await periodService.GetAllAsync();
            return View(periods);

        }

        // GET: Period/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            
            var period = await periodService.GetByIdAsync<PeriodReadOnlyVM>(id.Value);
            return period is null ? NotFound() : View(period);
        }

        // GET: Period/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Period/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeriodCreateVM periodCreate)
        {
            if (await periodService.CheckIfPeriodNameExists(periodCreate.Name))
                ModelState.AddModelError(nameof(periodCreate.Name), NameExistsValidationMessage);
            
            if (ModelState.IsValid)
            {
                await periodService.CreateAsync(periodCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(periodCreate);
        }

        // GET: Period/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await periodService.GetByIdAsync<PeriodEditVM>(id.Value);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Period/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeriodEditVM periodEdit)
        {
            if (id != periodEdit.Id)
            {
                return NotFound();
            }
            
            if (await periodService.CheckIfPeriodNameExists(periodEdit))
                ModelState.AddModelError(nameof(periodEdit.Name), NameExistsValidationMessage);


            if (ModelState.IsValid)
            {
                try
                {
                    await periodService.EditAsync(periodEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!periodService.PeriodExists(periodEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(periodEdit);
        }

        // GET: Period/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var period = await periodService.GetByIdAsync<PeriodReadOnlyVM>(id.Value);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Period/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await periodService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
