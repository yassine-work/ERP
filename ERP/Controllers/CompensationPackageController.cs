using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Services.IServiceContracts;
using ERP.Data;

namespace ERP.Controllers
{
    /// <summary>
    /// Domain Controller for Compensation Package Lifecycle
    /// RESPONSIBILITIES: Package versioning, history, state transitions, business rules
    /// ALL BUSINESS LOGIC DELEGATED TO SERVICES
    /// </summary>
    public class CompensationPackageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICompensationPackageService _compensationService;

        public CompensationPackageController(
            AppDbContext context,
            ICompensationPackageService compensationService)
        {
            _context = context;
            _compensationService = compensationService;
        }

        // ════════════════════════════════════════════════════════════
        // HISTORY (READ-ONLY)
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// Display all package versions for an employee
        /// IMMUTABLE - NO EDITING - NO DELETION
        /// Timeline view with version numbers
        /// </summary>
        public async Task<IActionResult> History(int employeeId)
        {
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound();

            var packages = await _context.CompensationPackages
                .Where(cp => cp.EmployeeId == employeeId)
                .OrderByDescending(cp => cp.EffectiveFrom)
                .Include(cp => cp.Allowances).ThenInclude(a => a.AllowanceType)
                .Include(cp => cp.Bonuses).ThenInclude(b => b.BonusType)
                .Include(cp => cp.Advantages).ThenInclude(adv => adv.AdvantageType)
                .ToListAsync();

            ViewBag.Employee = employee;
            return View(packages);
        }

        // ════════════════════════════════════════════════════════════
        // EDIT PACKAGE (VERSIONING OPERATION)
        // ════════════════════════════════════════════════════════════

        /// <summary>
        /// "Edit" Package (actually creates new version)
        /// Pre-fills form with current active package
        /// User perceives editing, system creates version
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int employeeId)
        {
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .Include(e => e.CompensationPackages)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound();

            // Get current active package to pre-fill form
            var activePackage = await _compensationService.GetActivePackageForEmployeeAsync(employeeId);

            // Load active package components if exists
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

            // Load dropdown data
            ViewBag.Employee = employee;
            ViewBag.ActivePackage = activePackage;
            ViewBag.AllowanceTypes = await _context.AllowanceTypes.OrderBy(t => t.AllowanceTypeName).ToListAsync();
            ViewBag.BonusTypes = await _context.BonusTypes.OrderBy(t => t.BonusTypeName).ToListAsync();
            ViewBag.AdvantageTypes = await _context.AdvantageTypes.OrderBy(t => t.AdvantageTypeName).ToListAsync();

            return View();
        }

        /// <summary>
        /// Save "edited" package (creates new version)
        /// BUSINESS RULE: Deactivate old + Create new + Archive
        /// ALL LOGIC DELEGATED TO SERVICE
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CompensationPackageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Données invalides. Veuillez vérifier le formulaire.";
                return RedirectToAction(nameof(Edit), new { employeeId = model.EmployeeId });
            }

            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == model.EmployeeId);

            if (employee == null)
                return NotFound();

            // Validate base salary against poste minimum
            if (model.BaseSalary < employee.Poste.MinimumBaseSalary)
            {
                TempData["ErrorMessage"] = $"Le salaire ne peut pas être inférieur au minimum du poste ({employee.Poste.MinimumBaseSalary:C}).";
                return RedirectToAction(nameof(Edit), new { employeeId = model.EmployeeId });
            }

            try
            {
                // ═══════════════════════════════════════════════════
                // VERSIONING LOGIC (ALL IN SERVICE)
                // ═══════════════════════════════════════════════════

                // 1. Get current active package
                var currentPackage = await _compensationService.GetActivePackageForEmployeeAsync(model.EmployeeId);

                // 2. Check if values have actually changed
                if (currentPackage != null && !await HasPackageChanged(currentPackage, model))
                {
                    TempData["InfoMessage"] = "Aucune modification détectée. Le package reste inchangé.";
                    return RedirectToAction("EmployeeView", "Paie", new { employeeId = model.EmployeeId });
                }

                // 3. Deactivate old package
                if (currentPackage != null)
                {
                    await _compensationService.DeactivatePackageAsync(currentPackage.Id);
                }

                // 4. Create new package
                var newPackage = new CompensationPackage
                {
                    EmployeeId = model.EmployeeId,
                    BaseSalary = model.BaseSalary,
                    EffectiveFrom = model.EffectiveFrom,
                    IsActive = true,
                    EffectiveTo = null
                };

                await _compensationService.SaveCompensationPackageAsync(newPackage);

                // 5. Add allowances to new package
                if (model.Allowances != null)
                {
                    foreach (var allowanceDto in model.Allowances)
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

                // 6. Add bonuses to new package
                if (model.Bonuses != null)
                {
                    foreach (var bonusDto in model.Bonuses)
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

                // 7. Add advantages to new package
                if (model.Advantages != null)
                {
                    foreach (var advantageDto in model.Advantages)
                    {
                        var advantage = new EmployeeAdvantage
                        {
                            CompensationPackageId = newPackage.Id,
                            AdvantageTypeId = advantageDto.AdvantageTypeId,
                            Value = advantageDto.Value,
                            IsActive = advantageDto.IsActive,
                            StartDate = advantageDto.StartDate,
                            EndDate = advantageDto.EndDate
                        };

                        await _compensationService.AddAdvantageAsync(newPackage.Id, advantage);
                    }
                }

                TempData["SuccessMessage"] = $"Package de rémunération mis à jour avec succès pour {employee.Nom} {employee.Prenom}.";
                
                // Return to employee main view
                return RedirectToAction("EmployeeView", "Paie", new { employeeId = model.EmployeeId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erreur lors de la création du package: {ex.Message}";
                return RedirectToAction(nameof(Edit), new { employeeId = model.EmployeeId });
            }
        }

        /// <summary>
        /// Compares current active package with the new model to detect actual changes
        /// Prevents duplicate versions when values haven't changed
        /// </summary>
        private async Task<bool> HasPackageChanged(CompensationPackage currentPackage, CompensationPackageViewModel newModel)
        {
            // Check base salary change
            if (currentPackage.BaseSalary != newModel.BaseSalary)
                return true;

            // Load current package relationships if not already loaded
            await _context.Entry(currentPackage)
                .Collection(cp => cp.Allowances)
                .LoadAsync();
            await _context.Entry(currentPackage)
                .Collection(cp => cp.Bonuses)
                .LoadAsync();
            await _context.Entry(currentPackage)
                .Collection(cp => cp.Advantages)
                .LoadAsync();

            // Check allowances count and content
            if ((currentPackage.Allowances?.Count ?? 0) != (newModel.Allowances?.Count ?? 0))
                return true;

            if (newModel.Allowances != null && currentPackage.Allowances != null)
            {
                foreach (var newAllowance in newModel.Allowances)
                {
                    var currentAllowance = currentPackage.Allowances
                        .FirstOrDefault(a => a.AllowanceTypeId == newAllowance.AllowanceTypeId);
                    
                    if (currentAllowance == null ||
                        currentAllowance.Amount != newAllowance.Amount ||
                        currentAllowance.IsRecurring != newAllowance.IsRecurring ||
                        currentAllowance.IsTaxable != newAllowance.IsTaxable ||
                        currentAllowance.Frequency != newAllowance.Frequency)
                    {
                        return true;
                    }
                }
            }

            // Check bonuses count and content
            if ((currentPackage.Bonuses?.Count ?? 0) != (newModel.Bonuses?.Count ?? 0))
                return true;

            if (newModel.Bonuses != null && currentPackage.Bonuses != null)
            {
                foreach (var newBonus in newModel.Bonuses)
                {
                    var currentBonus = currentPackage.Bonuses
                        .FirstOrDefault(b => b.BonusTypeId == newBonus.BonusTypeId);
                    
                    if (currentBonus == null ||
                        currentBonus.Amount != newBonus.Amount ||
                        currentBonus.IsAutomatic != newBonus.IsAutomatic ||
                        currentBonus.IsTaxable != newBonus.IsTaxable ||
                        currentBonus.IsExceptional != newBonus.IsExceptional ||
                        currentBonus.IsPerformanceBased != newBonus.IsPerformanceBased)
                    {
                        return true;
                    }
                }
            }

            // Check advantages count and content
            if ((currentPackage.Advantages?.Count ?? 0) != (newModel.Advantages?.Count ?? 0))
                return true;

            if (newModel.Advantages != null && currentPackage.Advantages != null)
            {
                foreach (var newAdvantage in newModel.Advantages)
                {
                    var currentAdvantage = currentPackage.Advantages
                        .FirstOrDefault(a => a.AdvantageTypeId == newAdvantage.AdvantageTypeId);
                    
                    if (currentAdvantage == null ||
                        currentAdvantage.Value != newAdvantage.Value ||
                        currentAdvantage.IsActive != newAdvantage.IsActive)
                    {
                        return true;
                    }
                }
            }

            // No changes detected
            return false;
        }
    }
}