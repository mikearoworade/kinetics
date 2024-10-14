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
    public class BranchesController : Controller
    {
        private readonly KineticsContext _context;

        public BranchesController (KineticsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var branches = _context.Branches
                .AsNoTracking();

            return View(await branches.ToListAsync());
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchDto branchDto)
        {
            if (ModelState.IsValid)
            {
                //Manually map DTO to the Branch Model
                var branch = new Branch
                {
                    BranchName = branchDto.BranchName,
                    BranchLocation = branchDto.BranchLocation
                };

                //Add the new branch to the context
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(branchDto);
        }

        // GET: Branches/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BranchDto branchDto)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchToUpdate = await _context.Branches.FirstOrDefaultAsync(b => b.BranchID == id);

            if (branchToUpdate == null)
            {
                return NotFound();
            }

            //Map the DTO values to the entity object
            branchToUpdate.BranchName = branchDto.BranchName;
            branchToUpdate.BranchLocation = branchDto.BranchLocation;

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
            return View(branchDto);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        //POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch != null)
            {
                _context.Remove(branch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
