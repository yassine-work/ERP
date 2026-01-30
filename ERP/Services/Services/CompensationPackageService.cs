using ERP.Models;
using ERP.Data;
using ERP.Services.IServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services
{
    public class CompensationPackageService : ICompensationPackageService
    {
        private readonly AppDbContext _context;

        public CompensationPackageService(AppDbContext context)
        {
            _context = context;
        }

        // ════════════════════════════════════════════════════════════
        // VERSIONING CORE LOGIC
        // ════════════════════════════════════════════════════════════

        public async Task<CompensationPackage> CreatePackageVersionAsync(
            int employeeId,
            decimal newBaseSalary,
            DateTime effectiveFrom,
            List<EmployeeAllowance>? allowances = null,
            List<EmployeeBonus>? bonuses = null,
            List<EmployeeAdvantage>? advantages = null)
        {
            // Business Rule: Deactivate current active package
            var currentActive = await GetActivePackageForEmployeeAsync(employeeId);
            if (currentActive != null)
            {
                await DeactivatePackageAsync(currentActive.Id);
            }

            // Create new package
            var newPackage = new CompensationPackage
            {
                EmployeeId = employeeId,
                BaseSalary = newBaseSalary,
                EffectiveFrom = effectiveFrom,
                IsActive = true,
                EffectiveTo = null
            };

            _context.CompensationPackages.Add(newPackage);
            await _context.SaveChangesAsync();

            // Add components
            if (allowances != null)
            {
                foreach (var allowance in allowances)
                {
                    allowance.CompensationPackageId = newPackage.Id;
                    await AddAllowanceAsync(newPackage.Id, allowance);
                }
            }

            if (bonuses != null)
            {
                foreach (var bonus in bonuses)
                {
                    bonus.CompensationPackageId = newPackage.Id;
                    await AddBonusAsync(newPackage.Id, bonus);
                }
            }

            if (advantages != null)
            {
                foreach (var advantage in advantages)
                {
                    advantage.CompensationPackageId = newPackage.Id;
                    await AddAdvantageAsync(newPackage.Id, advantage);
                }
            }

            return newPackage;
        }

        public async Task<CompensationPackage?> GetActivePackageForEmployeeAsync(int employeeId)
        {
            return await _context.CompensationPackages
                .FirstOrDefaultAsync(cp => cp.EmployeeId == employeeId && cp.IsActive);
        }

        public async Task DeactivatePackageAsync(int packageId)
        {
            var package = await _context.CompensationPackages.FindAsync(packageId);
            if (package == null)
                throw new InvalidOperationException($"Package {packageId} not found");

            package.IsActive = false;
            package.EffectiveTo = DateTime.Now;

            _context.Update(package);
            await _context.SaveChangesAsync();
        }

        public async Task ActivatePackageAsync(int packageId)
        {
            var package = await _context.CompensationPackages
                .Include(cp => cp.Employee)
                .FirstOrDefaultAsync(cp => cp.Id == packageId);

            if (package == null)
                throw new InvalidOperationException($"Package {packageId} not found");

            // Business Rule: Deactivate all other packages for this employee
            var otherPackages = await _context.CompensationPackages
                .Where(cp => cp.EmployeeId == package.EmployeeId && cp.Id != packageId)
                .ToListAsync();

            foreach (var other in otherPackages)
            {
                other.IsActive = false;
                other.EffectiveTo = DateTime.Now;
            }

            package.IsActive = true;
            package.EffectiveTo = null;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompensationPackage>> GetPackageHistoryAsync(int employeeId)
        {
            return await _context.CompensationPackages
                .Where(cp => cp.EmployeeId == employeeId)
                .OrderByDescending(cp => cp.EffectiveFrom)
                .Include(cp => cp.Allowances).ThenInclude(a => a.AllowanceType)
                .Include(cp => cp.Bonuses).ThenInclude(b => b.BonusType)
                .Include(cp => cp.Advantages).ThenInclude(adv => adv.AdvantageType)
                .ToListAsync();
        }

        // ════════════════════════════════════════════════════════════
        // COMPONENT MANAGEMENT
        // ════════════════════════════════════════════════════════════

        public async Task AddAllowanceAsync(int packageId, EmployeeAllowance allowance)
        {
            allowance.CompensationPackageId = packageId;
            _context.EmployeeAllowances.Add(allowance);
            await _context.SaveChangesAsync();
        }

        public async Task AddBonusAsync(int packageId, EmployeeBonus bonus)
        {
            bonus.CompensationPackageId = packageId;
            _context.EmployeeBonuses.Add(bonus);
            await _context.SaveChangesAsync();
        }

        public async Task AddAdvantageAsync(int packageId, EmployeeAdvantage advantage)
        {
            advantage.CompensationPackageId = packageId;
            _context.EmployeeAdvantages.Add(advantage);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAllowanceAsync(int allowanceId)
        {
            var allowance = await _context.EmployeeAllowances.FindAsync(allowanceId);
            if (allowance != null)
            {
                _context.EmployeeAllowances.Remove(allowance);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveBonusAsync(int bonusId)
        {
            var bonus = await _context.EmployeeBonuses.FindAsync(bonusId);
            if (bonus != null)
            {
                _context.EmployeeBonuses.Remove(bonus);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveAdvantageAsync(int advantageId)
        {
            var advantage = await _context.EmployeeAdvantages.FindAsync(advantageId);
            if (advantage != null)
            {
                _context.EmployeeAdvantages.Remove(advantage);
                await _context.SaveChangesAsync();
            }
        }

        // ════════════════════════════════════════════════════════════
        // BUSINESS CALCULATIONS
        // ════════════════════════════════════════════════════════════

        public async Task<decimal> CalculateTotalCompensationAsync(int packageId)
        {
            var package = await _context.CompensationPackages
                .Include(cp => cp.Allowances)
                .Include(cp => cp.Bonuses)
                .FirstOrDefaultAsync(cp => cp.Id == packageId);

            if (package == null)
                return 0;

            var total = package.BaseSalary;
            total += package.Allowances?.Sum(a => a.Amount) ?? 0;
            total += package.Bonuses?.Sum(b => b.Amount) ?? 0;

            return total;
        }

        public async Task<(bool IsValid, List<string> Errors)> ValidatePackageAsync(CompensationPackage package)
        {
            var errors = new List<string>();

            // Load employee and poste
            var employee = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == package.EmployeeId);

            if (employee == null)
            {
                errors.Add("Employee not found");
                return (false, errors);
            }

            // Validate base salary
            if (package.BaseSalary < employee.Poste.MinimumBaseSalary)
            {
                errors.Add($"Base salary ({package.BaseSalary:C}) is below minimum for {employee.Poste.Title} ({employee.Poste.MinimumBaseSalary:C})");
            }

            // Validate dates
            if (package.EffectiveTo.HasValue && package.EffectiveTo < package.EffectiveFrom)
            {
                errors.Add("EffectiveTo cannot be before EffectiveFrom");
            }

            return (errors.Count == 0, errors);
        }

        // ════════════════════════════════════════════════════════════
        // BASIC CRUD (Existing methods)
        // ════════════════════════════════════════════════════════════

        public async Task<CompensationPackage> SaveCompensationPackageAsync(CompensationPackage package)
        {
            _context.CompensationPackages.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<CompensationPackage> GetCompensationPackageByIdAsync(int id)
        {
            return await _context.CompensationPackages
                .Include(cp => cp.Employee)
                .Include(cp => cp.Allowances).ThenInclude(a => a.AllowanceType)
                .Include(cp => cp.Bonuses).ThenInclude(b => b.BonusType)
                .Include(cp => cp.Advantages).ThenInclude(adv => adv.AdvantageType)
                .FirstOrDefaultAsync(cp => cp.Id == id);
        }

        public async Task<IEnumerable<CompensationPackage>> GetAllCompensationPackagesAsync()
        {
            return await _context.CompensationPackages
                .Include(cp => cp.Employee)
                .ToListAsync();
        }

        public async Task<CompensationPackage> UpdateCompensationPackageAsync(CompensationPackage package)
        {
            _context.Update(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<bool> DeleteCompensationPackageAsync(int id)
        {
            var package = await _context.CompensationPackages.FindAsync(id);
            if (package == null)
                return false;

            _context.CompensationPackages.Remove(package);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}