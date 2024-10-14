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
    public class JobPositionsController : Controller
    {
        private readonly KineticsContext _context;

        public JobPositionsController(KineticsContext context)
        {
            _context = context;
        }

        // GET: JobPositions
        public async Task<IActionResult> Index()
        {
            var jobPositions = _context.JobPositions
                .AsNoTracking();

            return View(await jobPositions.ToListAsync());
        }

        // GET: JobPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobPositionDto jobPositionDto)
        {
            if (ModelState.IsValid)
            {
                // Manually Map DTO to JobPosition Entity
                var jobPosition = new JobPosition
                {
                    PositionName = jobPositionDto.PositionName,
                };

                //Add new position to Context
                _context.Add(jobPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobPositionDto);
        }

        // GET: JobPositions/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // POST: JobPositions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, JobPositionDto jobPositionDto)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchToUpdate = await _context.JobPositions.FindAsync(id);
            if (branchToUpdate == null)
            {
                return NotFound();
            }

            //Map Dto to JobPosition Entity
            branchToUpdate.PositionName = jobPositionDto.PositionName;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to Save changes. "
                    + "Try again, and if the problem persists, " + "See your system Administrator.");
            }
            return View(jobPositionDto);
        }

        // GET: JobPositions/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }
            var jobPosition = await _context.JobPositions.FindAsync(id);

            if(jobPosition == null)
            {
                return NotFound();
            }
            return View(jobPosition);
        }

        // POST: JobPosition/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);

            if (jobPosition != null)
            {
                _context.Remove(jobPosition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
