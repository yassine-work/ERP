using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Data;
using ERP.Services.IServiceContracts;

namespace ERP.Controllers
{
    public class PaieController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICompensationPackageService _compensationService;

        public PaieController(AppDbContext context, ICompensationPackageService compensationService)
        {
            _context = context;
            _compensationService = compensationService;
        }

        // GET: /Paie
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Paie/Search
        [HttpPost]
        public async Task<IActionResult> Search(string employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName))
            {
                TempData["ErrorMessage"] = "Please enter an employee name to search.";
                return RedirectToAction(nameof(Index));
            }

            var employees = await _context.Employes
                .Include(e => e.Poste)
                .Where(e => e.Nom.Contains(employeeName) || e.Prenom.Contains(employeeName))
                .ToListAsync();

            if (!employees.Any())
            {
                TempData["ErrorMessage"] = $"No employees found matching '{employeeName}'.";
                return RedirectToAction(nameof(Index));
            }

            if (employees.Count == 1)
            {
                return RedirectToAction(nameof(EditPackage), new { employeeId = employees[0].Id });
            }

            ViewBag.SearchTerm = employeeName;
            return View("SearchResults", employees);
        }

        // GET: /Paie/EmployeePackage/5
        public async Task<IActionResult> EmployeePackage(int employeeId)
        {
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null) return NotFound();

            var activePackage = await _compensationService.GetActivePackageForEmployeeAsync(employeeId);

            if (activePackage != null)
            {
                await _context.Entry(activePackage)
                    .Collection(cp => cp.Allowances)
                    .Query()
                    .Include(a => a.AllowanceType)
                    .LoadAsync();

                await _context.Entry(activePackage)
                    .Collection(cp => cp.Bonuses)
                    .Query()
                    .Include(b => b.BonusType)
                    .LoadAsync();

                await _context.Entry(activePackage)
                    .Collection(cp => cp.Advantages)
                    .Query()
                    .Include(adv => adv.AdvantageType)
                    .LoadAsync();
            }

            ViewBag.Employee = employee;
            ViewBag.ActivePackage = activePackage;

            return View();
        }

        // GET: /Paie/EditPackage/5
        public async Task<IActionResult> EditPackage(int employeeId)
        {
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null) return NotFound();

            var activePackage = await _compensationService.GetActivePackageForEmployeeAsync(employeeId);

            if (activePackage != null)
            {
                await _context.Entry(activePackage)
                    .Collection(cp => cp.Allowances)
                    .Query()
                    .Include(a => a.AllowanceType)
                    .LoadAsync();

                await _context.Entry(activePackage)
                    .Collection(cp => cp.Bonuses)
                    .Query()
                    .Include(b => b.BonusType)
                    .LoadAsync();

                await _context.Entry(activePackage)
                    .Collection(cp => cp.Advantages)
                    .Query()
                    .Include(adv => adv.AdvantageType)
                    .LoadAsync();
            }

            ViewBag.Employee = employee;
            ViewBag.ActivePackage = activePackage;
            ViewBag.AllowanceTypes = await _context.AllowanceTypes.ToListAsync();
            ViewBag.BonusTypes = await _context.BonusTypes.ToListAsync();
            ViewBag.AdvantageTypes = await _context.AdvantageTypes.ToListAsync();

            return View();
        }

        // POST: /Paie/SavePackage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePackage(
            int employeeId,
            decimal baseSalary,
            DateTime effectiveFrom,
            List<EmployeeAllowance>? allowances,
            List<EmployeeBonus>? bonuses,
            List<EmployeeAdvantage>? advantages)
        {
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null) return NotFound();

            if (baseSalary < employee.Poste.MinimumBaseSalary)
            {
                ModelState.AddModelError("", $"Base salary cannot be less than minimum ({employee.Poste.MinimumBaseSalary:C})");
                return RedirectToAction(nameof(EditPackage), new { employeeId });
            }

            // Deactivate current package
            var currentPackage = await _compensationService.GetActivePackageForEmployeeAsync(employeeId);
            if (currentPackage != null)
            {
                await _compensationService.DeactivatePackageAsync(currentPackage.Id);
            }

            // Create new package under the hood
            var newPackage = new CompensationPackage
            {
                EmployeeId = employeeId,
                BaseSalary = baseSalary,
                EffectiveFrom = effectiveFrom,
                IsActive = true,
                EffectiveTo = null
            };

            await _compensationService.SaveCompensationPackageAsync(newPackage);

            // Add allowances
            if (allowances != null && allowances.Any())
            {
                foreach (var allowanceDto in allowances)
                {
                    var allowance = new EmployeeAllowance
                    {
                        CompensationPackageId = newPackage.Id,
                        AllowanceTypeId = allowanceDto.AllowanceTypeId,
                        Amount = allowanceDto.Amount,
                        IsRecurring = allowanceDto.IsRecurring,
                        IsTaxable = allowanceDto.IsTaxable,
                        Frequency = allowanceDto.Frequency,
                        EffectiveFrom = allowanceDto.EffectiveFrom,
                        EffectiveTo = allowanceDto.EffectiveTo
                    };
                    await _compensationService.AddAllowanceAsync(newPackage.Id, allowance);
                }
            }

            // Add bonuses
            if (bonuses != null && bonuses.Any())
            {
                foreach (var bonusDto in bonuses)
                {
                    var bonus = new EmployeeBonus
                    {
                        CompensationPackageId = newPackage.Id,
                        BonusTypeId = bonusDto.BonusTypeId,
                        Amount = bonusDto.Amount,
                        IsAutomatic = bonusDto.IsAutomatic,
                        IsTaxable = bonusDto.IsTaxable,
                        IsExceptional = bonusDto.IsExceptional,
                        IsPerformanceBased = bonusDto.IsPerformanceBased,
                        BonusRule = bonusDto.BonusRule,
                        AwardedOn = bonusDto.AwardedOn,
                        ValidUntil = bonusDto.ValidUntil
                    };
                    await _compensationService.AddBonusAsync(newPackage.Id, bonus);
                }
            }

            // Add advantages
            if (advantages != null && advantages.Any())
            {
                foreach (var advDto in advantages)
                {
                    var adv = new EmployeeAdvantage
                    {
                        CompensationPackageId = newPackage.Id,
                        AdvantageTypeId = advDto.AdvantageTypeId,
                        Value = advDto.Value,
                        IsActive = advDto.IsActive,
                        StartDate = advDto.StartDate,
                        EndDate = advDto.EndDate
                    };
                    await _compensationService.AddAdvantageAsync(newPackage.Id, adv);
                }
            }

            TempData["SuccessMessage"] = $"Package saved for {employee.Nom} {employee.Prenom}";
            return RedirectToAction(nameof(EditPackage), new { employeeId });
        }

        // GET: /Paie/History/5
        public async Task<IActionResult> History(int employeeId)
        {
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null) return NotFound();

            var packages = await _context.CompensationPackages
                .Where(cp => cp.EmployeeId == employeeId)
                .OrderByDescending(cp => cp.EffectiveFrom)
                .Include(cp => cp.Allowances)
                    .ThenInclude(a => a.AllowanceType)
                .Include(cp => cp.Bonuses)
                    .ThenInclude(b => b.BonusType)
                .Include(cp => cp.Advantages)
                    .ThenInclude(adv => adv.AdvantageType)
                .ToListAsync();

            ViewBag.Employee = employee;
            return View(packages);
        }
    }
}

    