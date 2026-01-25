using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Data;
using ERP.Services.IServiceContracts;

namespace ERP.Controllers
{
    public class CompensationPackageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICompensationPackageService _compensationService;

        public CompensationPackageController(AppDbContext context, ICompensationPackageService compensationService)
        {
            _context = context;
            _compensationService = compensationService;
        }

        // GET: CompensationPackage/Details/5
        public async Task<IActionResult> Details(int employeeId)
        {
            var employee = await _context.Employes.Include(e => e.Poste).FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null) return NotFound();

            var activePackage = await _compensationService.GetActivePackageForEmployeeAsync(employeeId);
            if (activePackage == null)
            {
                TempData["ErrorMessage"] = "No active compensation package found for this employee.";
                return RedirectToAction("Details", "Employes", new { id = employeeId });
            }

            ViewBag.Employee = employee;
            return View(activePackage);
        }

        // GET: CompensationPackage/EditBaseSalary/5
        public async Task<IActionResult> EditBaseSalary(int packageId)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            await _context.Entry(package).Reference(p => p.Employee).LoadAsync();
            await _context.Entry(package.Employee).Reference(e => e.Poste).LoadAsync();

            ViewBag.MinimumSalary = package.Employee.Poste.MinimumBaseSalary;
            return View(package);
        }

        // POST: CompensationPackage/EditBaseSalary/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBaseSalary(int packageId, decimal newBaseSalary)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            await _context.Entry(package).Reference(p => p.Employee).LoadAsync();
            await _context.Entry(package.Employee).Reference(e => e.Poste).LoadAsync();

            if (newBaseSalary < package.Employee.Poste.MinimumBaseSalary)
            {
                ModelState.AddModelError("", $"Base salary cannot be less than the minimum ({package.Employee.Poste.MinimumBaseSalary:C})");
                ViewBag.MinimumSalary = package.Employee.Poste.MinimumBaseSalary;
                return View(package);
            }

            // Clone package with updated salary
            var newPackage = ClonePackage(package);
            newPackage.BaseSalary = newBaseSalary;
            await ActivateNewPackage(package, newPackage);

            TempData["SuccessMessage"] = $"Base salary updated to {newBaseSalary:C}";
            return RedirectToAction(nameof(Details), new { employeeId = package.EmployeeId });
        }

        // GET: AddAllowance
        public async Task<IActionResult> AddAllowance(int packageId)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            ViewBag.Package = package;
            ViewBag.AllowanceTypes = await _context.AllowanceTypes.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAllowance(int packageId, int allowanceTypeId, decimal amount,
            bool isRecurring, bool isTaxable, string? frequency, DateTime effectiveFrom, DateTime? effectiveTo)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            var allowanceType = await _context.AllowanceTypes.FindAsync(allowanceTypeId);
            if (allowanceType == null) return RedirectToAction(nameof(AddAllowance), new { packageId });

            var newPackage = ClonePackage(package);
            newPackage.Allowances.Add(new EmployeeAllowance
            {
                AllowanceTypeId = allowanceTypeId,
                Amount = amount,
                IsRecurring = isRecurring,
                IsTaxable = isTaxable,
                Frequency = frequency,
                EffectiveFrom = effectiveFrom,
                EffectiveTo = effectiveTo
            });

            await ActivateNewPackage(package, newPackage);
            TempData["SuccessMessage"] = $"Allowance '{allowanceType.AllowanceTypeName}' added successfully";
            return RedirectToAction(nameof(Details), new { employeeId = package.EmployeeId });
        }

        // GET: AddBonus
        public async Task<IActionResult> AddBonus(int packageId)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            ViewBag.Package = package;
            ViewBag.BonusTypes = await _context.BonusTypes.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBonus(int packageId, int bonusTypeId, decimal amount,
            bool isAutomatic, bool isTaxable, bool isExceptional, bool isPerformanceBased,
            string? bonusRule, DateTime awardedOn, DateTime? validUntil)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            var bonusType = await _context.BonusTypes.FindAsync(bonusTypeId);
            if (bonusType == null) return RedirectToAction(nameof(AddBonus), new { packageId });

            var newPackage = ClonePackage(package);
            newPackage.Bonuses.Add(new EmployeeBonus
            {
                BonusTypeId = bonusTypeId,
                Amount = amount,
                IsAutomatic = isAutomatic,
                IsTaxable = isTaxable,
                IsExceptional = isExceptional,
                IsPerformanceBased = isPerformanceBased,
                BonusRule = bonusRule,
                AwardedOn = awardedOn,
                ValidUntil = validUntil
            });

            await ActivateNewPackage(package, newPackage);
            TempData["SuccessMessage"] = $"Bonus '{bonusType.BonusTypeName}' added successfully";
            return RedirectToAction(nameof(Details), new { employeeId = package.EmployeeId });
        }

        // GET: AddAdvantage
        public async Task<IActionResult> AddAdvantage(int packageId)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            ViewBag.Package = package;
            ViewBag.AdvantageTypes = await _context.AdvantageTypes.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdvantage(int packageId, int advantageTypeId, decimal value,
            string? provider, string? eligibilityRule, bool isActive, DateTime startDate, DateTime? endDate)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            var advType = await _context.AdvantageTypes.FindAsync(advantageTypeId);
            if (advType == null) return RedirectToAction(nameof(AddAdvantage), new { packageId });

            var newPackage = ClonePackage(package);
            newPackage.Advantages.Add(new EmployeeAdvantage
            {
                AdvantageTypeId = advantageTypeId,
                Value = value,
                
                IsActive = isActive,
                StartDate = startDate,
                EndDate = endDate
            });

            await ActivateNewPackage(package, newPackage);
            TempData["SuccessMessage"] = $"Advantage '{advType.AdvantageTypeName}' added successfully";
            return RedirectToAction(nameof(Details), new { employeeId = package.EmployeeId });
        }

        // POST: Remove components
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAllowance(int allowanceId, int packageId)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            var newPackage = ClonePackage(package);
            var toRemove = newPackage.Allowances.FirstOrDefault(a => a.EmployeeAllowanceId == allowanceId);
            if (toRemove != null) newPackage.Allowances.Remove(toRemove);

            await ActivateNewPackage(package, newPackage);
            TempData["SuccessMessage"] = "Allowance removed successfully";
            return RedirectToAction(nameof(Details), new { employeeId = package.EmployeeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBonus(int bonusId, int packageId)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            var newPackage = ClonePackage(package);
            var toRemove = newPackage.Bonuses.FirstOrDefault(b => b.EmployeeBonusId == bonusId);
            if (toRemove != null) newPackage.Bonuses.Remove(toRemove);

            await ActivateNewPackage(package, newPackage);
            TempData["SuccessMessage"] = "Bonus removed successfully";
            return RedirectToAction(nameof(Details), new { employeeId = package.EmployeeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAdvantage(int advantageId, int packageId)
        {
            var package = await _compensationService.GetCompensationPackageByIdAsync(packageId);
            if (package == null) return NotFound();

            var newPackage = ClonePackage(package);
            var toRemove = newPackage.Advantages.FirstOrDefault(a => a.EmployeeAdvantageId == advantageId);
            if (toRemove != null) newPackage.Advantages.Remove(toRemove);

            await ActivateNewPackage(package, newPackage);
            TempData["SuccessMessage"] = "Advantage removed successfully";
            return RedirectToAction(nameof(Details), new { employeeId = package.EmployeeId });
        }

        // Helper: Clone package
        private CompensationPackage ClonePackage(CompensationPackage package)
        {
            return new CompensationPackage
            {
                EmployeeId = package.EmployeeId,
                BaseSalary = package.BaseSalary,
                EffectiveFrom = DateTime.Now,
                IsActive = true,
                Advantages = package.Advantages?.Select(a => new EmployeeAdvantage
                {
                    AdvantageTypeId = a.AdvantageTypeId,
                    Value = a.Value,
                    
                    IsActive = a.IsActive,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                }).ToList() ?? new List<EmployeeAdvantage>(),
                Allowances = package.Allowances?.Select(a => new EmployeeAllowance
                {
                    AllowanceTypeId = a.AllowanceTypeId,
                    Amount = a.Amount,
                    IsRecurring = a.IsRecurring,
                    IsTaxable = a.IsTaxable,
                    Frequency = a.Frequency,
                    EffectiveFrom = a.EffectiveFrom,
                    EffectiveTo = a.EffectiveTo
                }).ToList() ?? new List<EmployeeAllowance>(),
                Bonuses = package.Bonuses?.Select(b => new EmployeeBonus
                {
                    BonusTypeId = b.BonusTypeId,
                    Amount = b.Amount,
                    IsAutomatic = b.IsAutomatic,
                    IsTaxable = b.IsTaxable,
                    IsExceptional = b.IsExceptional,
                    IsPerformanceBased = b.IsPerformanceBased,
                    BonusRule = b.BonusRule,
                    AwardedOn = b.AwardedOn,
                    ValidUntil = b.ValidUntil
                }).ToList() ?? new List<EmployeeBonus>()
            };
        }

        // Helper: deactivate old and save new
        private async Task ActivateNewPackage(CompensationPackage oldPackage, CompensationPackage newPackage)
        {
            oldPackage.IsActive = false;
            oldPackage.EffectiveTo = DateTime.Now;

            _context.CompensationPackages.Add(newPackage);
            await _context.SaveChangesAsync();
        }
    }
}
