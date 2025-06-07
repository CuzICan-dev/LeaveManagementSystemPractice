using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystemPractice.web.Data;
using LeaveManagementSystemPractice.web.Data.Entities;
using LeaveManagementSystemPractice.web.Models.LeaveTypes;

namespace LeaveManagementSystemPractice.web.Controllers
{
    public class LeaveTypeController(ApplicationDbContext context, IMapper mapper) : Controller
    {
        private const string NameExistsValidationMessage = "Name already exists in the database";
        // GET: LeaveType
        public async Task<IActionResult> Index()
        {
            var leaveTypes = await context.LeaveTypes.ToListAsync();
            List<LeaveTypeReadOnlyVM> leaveTypeViewModels = mapper.Map<List<LeaveTypeReadOnlyVM>>(leaveTypes);
            return View(leaveTypeViewModels);
        }

        // GET: LeaveType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            LeaveTypeReadOnlyVM leaveTypeViewModel = mapper.Map<LeaveTypeReadOnlyVM>(leaveType);
            return View(leaveTypeViewModel);
        }

        // GET: LeaveType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            if (await context.LeaveTypes.AnyAsync(e => e.Name.ToLower().Equals(leaveTypeCreate.Name.ToLower())))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsValidationMessage);
            }
            if (ModelState.IsValid)
            {
                LeaveType leaveType = mapper.Map<LeaveType>(leaveTypeCreate);
                context.Add(leaveType);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreate);
        }

        // GET: LeaveType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            LeaveTypeEditVM leaveTypeEdit = mapper.Map<LeaveTypeEditVM>(leaveType);
            return View(leaveTypeEdit);
        }

        // POST: LeaveType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await context.LeaveTypes.AnyAsync(e => e.Name.ToLower().Equals(leaveTypeEdit.Name.ToLower()) && e.Id != leaveTypeEdit.Id))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var leaveType = mapper.Map<LeaveType>(leaveTypeEdit);
                    context.Update(leaveType);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var leaveType = mapper.Map<LeaveType>(leaveTypeEdit);
                    if (!LeaveTypeExists(leaveType.Id))
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
            return View(leaveTypeEdit);
        }

        // GET: LeaveType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            LeaveTypeReadOnlyVM leaveTypeViewModel = mapper.Map<LeaveTypeReadOnlyVM>(leaveType);
            return View(leaveType);
        }

        // POST: LeaveType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                context.LeaveTypes.Remove(leaveType);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeExists(int id)
        {
            return context.LeaveTypes.Any(e => e.Id == id);
        }
    }
}
