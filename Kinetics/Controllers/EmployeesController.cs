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
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Kinetics.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly KineticsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeesController(KineticsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Employees
                .Include(e => e.Branch)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Include(e => e.LineManager)
                .Include(e => e.JobPosition);

            return View(await employeeContext.ToListAsync());
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            // Get list of employees who are eligible to be Line Managers based on their role or job position
            var eligibleManagers = await _context.Employees
                                        .Where(e => e.Role.RoleName == "Manager" || e.Role.RoleName == "Supervisor")
                                        .ToListAsync();

            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchName");
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            ViewData["PositionID"] = new SelectList(_context.JobPositions, "JobPositionID", "PositionName");
            ViewData["LineManagerID"] = new SelectList(eligibleManagers, "EmployeeID", "FullName");
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName");
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto employeeDto, IFormFile photoFile)
        {
            // Get list of employees who are eligible to be Line Managers based on their role or job position
            var eligibleManagers = await _context.Employees
                                        .Where(e => e.Role.RoleName == "Manager" || e.Role.RoleName == "Supervisor")
                                        .ToListAsync();

            if (ModelState.IsValid)
            {
                if (photoFile != null && photoFile.Length > 0)
                {
                    // Get the file name without extension
                    var fileName = Path.GetFileNameWithoutExtension(photoFile.FileName).Trim();

                    // Replace spaces with underscores
                    fileName = fileName.Replace(" ", "_");

                    // Append a unique identifier and retain the original file extension
                    var extension = Path.GetExtension(photoFile.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    // Determine the path to save the file
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //Ensure the directory exixts
                    Directory.CreateDirectory(uploadsFolder);

                    // Copy the uploaded file to the target folder
                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await photoFile.CopyToAsync(fileStream);
                    }

                    // Save the file name in the Employee's Photo property
                    employeeDto.Photo = "/images/" + uniqueFileName;
                }
                // Manually map DTO to the Department Model
                var employee = new Employee
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    StaffID = employeeDto.StaffID,
                    Phone = employeeDto.Phone,
                    HireDate = employeeDto.HireDate,
                    DepartmentID = employeeDto.DepartmentID,
                    RoleID = employeeDto.RoleID,
                    PositionID = employeeDto.PositionID,
                    BranchID = employeeDto.BranchID,
                    LineManagerID = employeeDto.LineManagerID,
                    Photo = employeeDto.Photo
                };

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchName");
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            ViewData["PositionID"] = new SelectList(_context.JobPositions, "JobPositionID", "PositionName");
            ViewData["LineManagerID"] = new SelectList(eligibleManagers, "EmployeeID", "FullName");
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName");
            return View(employeeDto);
        }


        // GET: Employees/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get list of employees who are eligible to be Line Managers based on their role or job position
            var eligibleManagers = await _context.Employees
                                        .Where(e => e.Role.RoleName == "Manager" || e.Role.RoleName == "Supervisor")
                                        .ToListAsync();
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchName");
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            ViewData["PositionID"] = new SelectList(_context.JobPositions, "JobPositionID", "PositionName");
            ViewData["LineManagerID"] = new SelectList(eligibleManagers, "EmployeeID", "FullName");
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName");
            return View(employee);
        }

        // POST: Employees/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeDto employeeDto, IFormFile photoFile)
        {
            var employeeToUpdate = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
            if (employeeToUpdate == null)
            {
                return NotFound();
            }

            if (photoFile != null && photoFile.Length > 0)
            {
                // Delete the old photo if it exists
                if (employeeToUpdate.Photo != null)
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, employeeToUpdate.Photo.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                // Get the file name without extension
                var fileName = Path.GetFileNameWithoutExtension(photoFile.FileName).Trim();

                // Replace spaces with underscores
                fileName = fileName.Replace(" ", "_");

                // Append a unique identifier and retain the original file extension
                var extension = Path.GetExtension(photoFile.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Determine the path to save the file
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //Ensure the directory exixts
                Directory.CreateDirectory(uploadsFolder);

                // Copy the uploaded file to the target folder
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(fileStream);
                }

                // Save the file name in the Employee's Photo property
                employeeToUpdate.Photo = "/images/" + uniqueFileName;
            }

            // Map DTO the Employee Model
            employeeToUpdate.FirstName = employeeDto.FirstName;
            employeeToUpdate.LastName = employeeDto.LastName;
            employeeToUpdate.Email = employeeDto.Email;
            employeeToUpdate.StaffID = employeeDto.StaffID;
            employeeToUpdate.Phone = employeeDto.Phone;
            employeeToUpdate.HireDate = employeeDto.HireDate;
            employeeToUpdate.DepartmentID = employeeDto.DepartmentID;
            employeeToUpdate.RoleID = employeeDto.RoleID;
            employeeToUpdate.PositionID = employeeDto.PositionID;
            employeeToUpdate.BranchID = employeeDto.BranchID;
            employeeToUpdate.LineManagerID = employeeDto.LineManagerID;

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

            return View(employeeToUpdate);
        }
    }

}
