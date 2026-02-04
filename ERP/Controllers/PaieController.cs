using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Data;

namespace ERP.Controllers
{
    /// <summary>
    /// UI Orchestrator for Payroll & Compensation
    /// RESPONSIBILITIES: Navigation, Search, View Composition, Routing
    /// NO BUSINESS LOGIC - NO PACKAGE MANIPULATION - NO DOMAIN OPERATIONS
    /// </summary>
    public class PaieController : Controller
    {
        private readonly AppDbContext _context;

        public PaieController(AppDbContext context)
        {
            _context = context;
        }

        // ════════════════════════════════════════════════════════════
        // SEARCH FLOW
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// Landing page - ONLY search bar
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search employees by name
        /// Returns: Single → Auto-select | Multiple → List
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Search(string employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName))
            {
                TempData["ErrorMessage"] = "Veuillez entrer un nom d'employé.";
                return RedirectToAction(nameof(Index));
            }

            var searchTerm = employeeName.ToLower();
            var employees = await _context.Employes
                .Include(e => e.Poste)
                .Where(e => e.Nom.ToLower().Contains(searchTerm) || e.Prenom.ToLower().Contains(searchTerm))
                .ToListAsync();

            if (!employees.Any())
            {
                TempData["ErrorMessage"] = $"Aucun employé trouvé pour '{employeeName}'.";
                return RedirectToAction(nameof(Index));
            }

            // Single match → auto-select
            if (employees.Count == 1)
            {
                return RedirectToAction(nameof(EmployeeView), new { employeeId = employees[0].Id });
            }

            // Multiple matches → show list
            ViewBag.SearchTerm = employeeName;
            return View("SearchResults", employees);
        }

        // ════════════════════════════════════════════════════════════
        // EMPLOYEE VIEW (MAIN SCREEN - READ ONLY)
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// Main employee compensation view
        /// DISPLAYS: Employee info + Active package (READ-ONLY)
        /// ROUTES TO: History, Edit Package
        /// NO FORMS - NO EDITING - PURE DISPLAY
        /// </summary>
        public async Task<IActionResult> EmployeeView(int employeeId)
        {
            // Load employee
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound();

            // Load active package (READ ONLY)
            var activePackage = await _context.CompensationPackages
                .Include(cp => cp.Allowances).ThenInclude(a => a.AllowanceType)
                .Include(cp => cp.Bonuses).ThenInclude(b => b.BonusType)
                .Include(cp => cp.Advantages).ThenInclude(adv => adv.AdvantageType)
                .FirstOrDefaultAsync(cp => cp.EmployeeId == employeeId && cp.IsActive);

            ViewBag.Employee = employee;
            ViewBag.ActivePackage = activePackage;

            return View();
        }

        // ════════════════════════════════════════════════════════════
        // ROUTING ONLY - NO BUSINESS LOGIC
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// Route to History (handled by CompensationPackageController)
        /// This is just a convenience route - could be removed
        /// </summary>
        public IActionResult History(int employeeId)
        {
            return RedirectToAction("History", "CompensationPackage", new { employeeId });
        }

        /// <summary>
        /// Route to Edit Package (handled by CompensationPackageController)
        /// This is just a convenience route - could be removed
        /// </summary>
        public IActionResult EditPackage(int employeeId)
        {
            return RedirectToAction("Edit", "CompensationPackage", new { employeeId });
        }
    }
}