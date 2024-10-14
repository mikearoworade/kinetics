using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kinetics.Data;
using Kinetics.Models;
using Kinetics.Models.DTO;

namespace Kinetics.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly KineticsContext _context;

        public DepartmentsController(KineticsContext context)
        {
            _context = context;
        }

       // GET: Departments/
       public async Task<IActionResult> Index()
        {
            var departments = _context.Departments
                .AsNoTracking();

            return View(await departments.ToListAsync());
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                // Manually map DTO to the Department Model
                var department = new Department
                {
                    DepartmentName = departmentDto.DepartmentName
                };

                // Add the new Department to Context
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(departmentDto);
        }

        // GET: Departments/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(); 
            }
            return View(department);
        }

        // POST: Departments/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, DepartmentDto departmentDto)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentToUpdate = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (departmentToUpdate == null)
            {
                return NotFound();
            }

            // Map DTO to Model
            departmentToUpdate.DepartmentName = departmentDto.DepartmentName;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, "
                    + "See your system administrator.");
            }
            return View(departmentDto);
        }

        // GET: Departments/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                NotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Remove(department);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
