using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using ERP.Models;

namespace ERP.Controllers
{
    public class EquipmentAssignmentController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentAssignmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentAssignment
        public async Task<IActionResult> Index(int? employeeId, bool? activeOnly)
        {
            var query = _context.EquipmentAssignments
                .Include(a => a.Equipment)
                    .ThenInclude(e => e.EquipmentType)
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Poste)
                .AsQueryable();

            if (employeeId.HasValue)
            {
                query = query.Where(a => a.EmployeeId == employeeId.Value);
            }

            if (activeOnly == true)
            {
                query = query.Where(a => a.ReturnDate == null);
            }

            ViewBag.Employees = await _context.Employes
                .OrderBy(e => e.Nom)
                .ThenBy(e => e.Prenom)
                .ToListAsync();
            ViewBag.EmployeeId = employeeId;
            ViewBag.ActiveOnly = activeOnly;

            var assignments = await query
                .OrderByDescending(a => a.AssignmentDate)
                .ToListAsync();

            return View(assignments);
        }

        // GET: EquipmentAssignment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.EquipmentAssignments
                .Include(a => a.Equipment)
                    .ThenInclude(e => e.EquipmentType)
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Poste)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);

            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: EquipmentAssignment/EmployeeEquipment/5
        public async Task<IActionResult> EmployeeEquipment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employes
                .Include(e => e.Poste)
                .Include(e => e.EquipmentAssignments)
                    .ThenInclude(a => a.Equipment)
                        .ThenInclude(eq => eq.EquipmentType)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
    }
}
