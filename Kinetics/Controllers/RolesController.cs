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
    public class RolesController : Controller
    {
        private readonly KineticsContext _context;

        public RolesController(KineticsContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = _context.Roles
                .AsNoTracking();

            return View(await roles.ToListAsync());
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to Role Model
                var role = new Role
                {
                    RoleName = roleDto.RoleName
                };

                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleDto);
        }

        // GET: Role/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Role/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, RoleDto roleDto)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var roleToUpdate = await _context.Roles.FirstOrDefaultAsync(r => r.RoleID == id);

                if (roleToUpdate == null)
                {
                    return NotFound();
                }

                // Map Dto to Role Model
                roleToUpdate.RoleName = roleDto.RoleName;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to Update Role Model");
                }
            }
            return View(roleDto);
        }

        // GET: Role/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _context.Roles.FindAsync(id);
            if(role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Role/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfrmed(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Remove(role);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
