using ERP.Data;
using ERP.Models;
using Microsoft.EntityFrameworkCore;
using ERP.Services.IServiceContracts;

namespace ERP.Services
{
    public class CompensationPackageService : ICompensationPackageService
    {
        private readonly AppDbContext _context;

        public CompensationPackageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CompensationPackage> SaveCompensationPackageAsync(CompensationPackage package)
        {
            // Ensure only one active package per employee
            if (package.IsActive)
            {
                var active = await _context.CompensationPackages
                    .Where(p => p.EmployeeId == package.EmployeeId && p.IsActive)
                    .FirstOrDefaultAsync();

                if (active != null)
                {
                    active.IsActive = false;
                    active.EffectiveTo = DateTime.Now;
                }
            }

            _context.CompensationPackages.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<CompensationPackage> GetCompensationPackageByIdAsync(int id)
        {
            return await _context.CompensationPackages
                .Include(p => p.Employee)
                .Include(p => p.Advantages)
                    .ThenInclude(a => a.AdvantageType)
                .Include(p => p.Allowances)
                    .ThenInclude(a => a.AllowanceType)
                .Include(p => p.Bonuses)
                    .ThenInclude(b => b.BonusType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<CompensationPackage>> GetAllCompensationPackagesAsync()
        {
            return await _context.CompensationPackages
                .Include(p => p.Employee)
                .Include(p => p.Advantages)
                    .ThenInclude(a => a.AdvantageType)
                .Include(p => p.Allowances)
                    .ThenInclude(a => a.AllowanceType)
                .Include(p => p.Bonuses)
                    .ThenInclude(b => b.BonusType)
                .ToListAsync();
        }

        public async Task<CompensationPackage> UpdateCompensationPackageAsync(CompensationPackage package)
        {
            // Never edit active package in-place, create new version instead
            var oldPackage = await _context.CompensationPackages
                .Include(p => p.Advantages)
                .Include(p => p.Allowances)
                .Include(p => p.Bonuses)
                .FirstOrDefaultAsync(p => p.Id == package.Id);

            if (oldPackage == null)
                throw new Exception("Package not found");

            // Deactivate old
            oldPackage.IsActive = false;
            oldPackage.EffectiveTo = DateTime.Now;

            // Clone for new version
            var newPackage = new CompensationPackage
            {
                EmployeeId = oldPackage.EmployeeId,
                BaseSalary = package.BaseSalary,
                EffectiveFrom = DateTime.Now,
                IsActive = true,
                Advantages = oldPackage.Advantages.Select(a => new EmployeeAdvantage
                {
                    AdvantageTypeId = a.AdvantageTypeId,
                    Value = a.Value,
                   
                    IsActive = a.IsActive,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                }).ToList(),
                Allowances = oldPackage.Allowances.Select(a => new EmployeeAllowance
                {
                    AllowanceTypeId = a.AllowanceTypeId,
                    Amount = a.Amount,
                    IsRecurring = a.IsRecurring,
                    IsTaxable = a.IsTaxable,
                    Frequency = a.Frequency,
                    EffectiveFrom = a.EffectiveFrom,
                    EffectiveTo = a.EffectiveTo
                }).ToList(),
                Bonuses = oldPackage.Bonuses.Select(b => new EmployeeBonus
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
                }).ToList()
            };

            _context.CompensationPackages.Add(newPackage);
            await _context.SaveChangesAsync();
            return newPackage;
        }

        public async Task<bool> DeleteCompensationPackageAsync(int id)
        {
            var package = await _context.CompensationPackages.FindAsync(id);
            if (package == null) return false;

            // Never delete active package directly
            if (package.IsActive)
                throw new Exception("Cannot delete active package");

            _context.CompensationPackages.Remove(package);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CompensationPackage?> GetActivePackageForEmployeeAsync(int employeeId)
        {
            return await _context.CompensationPackages
                .Include(p => p.Advantages)
                    .ThenInclude(a => a.AdvantageType)
                .Include(p => p.Allowances)
                    .ThenInclude(a => a.AllowanceType)
                .Include(p => p.Bonuses)
                    .ThenInclude(b => b.BonusType)
                .FirstOrDefaultAsync(p => p.EmployeeId == employeeId && p.IsActive);
        }

        public async Task ActivatePackageAsync(int packageId)
        {
            var package = await _context.CompensationPackages
                .Include(p => p.Employee)
                .Include(p => p.Advantages)
                .Include(p => p.Allowances)
                .Include(p => p.Bonuses)
                .FirstOrDefaultAsync(p => p.Id == packageId);

            if (package == null)
                throw new Exception($"Package {packageId} not found");

            // Deactivate current active
            var currentActive = await _context.CompensationPackages
                .Where(p => p.EmployeeId == package.EmployeeId && p.IsActive)
                .FirstOrDefaultAsync();

            if (currentActive != null)
            {
                currentActive.IsActive = false;
                currentActive.EffectiveTo = DateTime.Now;
            }

            // Clone package as new version
            var newPackage = new CompensationPackage
            {
                EmployeeId = package.EmployeeId,
                BaseSalary = package.BaseSalary,
                EffectiveFrom = DateTime.Now,
                IsActive = true,
                Advantages = package.Advantages.Select(a => new EmployeeAdvantage
                {
                    AdvantageTypeId = a.AdvantageTypeId,
                    Value = a.Value,
                    
                    IsActive = a.IsActive,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                }).ToList(),
                Allowances = package.Allowances.Select(a => new EmployeeAllowance
                {
                    AllowanceTypeId = a.AllowanceTypeId,
                    Amount = a.Amount,
                    IsRecurring = a.IsRecurring,
                    IsTaxable = a.IsTaxable,
                    Frequency = a.Frequency,
                    EffectiveFrom = a.EffectiveFrom,
                    EffectiveTo = a.EffectiveTo
                }).ToList(),
                Bonuses = package.Bonuses.Select(b => new EmployeeBonus
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
                }).ToList()
            };

            _context.CompensationPackages.Add(newPackage);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivatePackageAsync(int packageId)
        {
            var package = await _context.CompensationPackages.FindAsync(packageId);
            if (package == null) throw new Exception($"Package {packageId} not found");

            package.IsActive = false;
            package.EffectiveTo = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task AddBonusAsync(int packageId, EmployeeBonus bonus)
        {
            var package = await _context.CompensationPackages
                .Include(p => p.Bonuses)
                .FirstOrDefaultAsync(p => p.Id == packageId);

            if (package == null) throw new Exception("Package not found");
            package.Bonuses ??= new List<EmployeeBonus>();
            package.Bonuses.Add(bonus);
            await _context.SaveChangesAsync();
        }

        public async Task AddAllowanceAsync(int packageId, EmployeeAllowance allowance)
        {
            var package = await _context.CompensationPackages
                .Include(p => p.Allowances)
                .FirstOrDefaultAsync(p => p.Id == packageId);

            if (package == null) throw new Exception("Package not found");
            package.Allowances ??= new List<EmployeeAllowance>();
            package.Allowances.Add(allowance);
            await _context.SaveChangesAsync();
        }

        public async Task AddAdvantageAsync(int packageId, EmployeeAdvantage advantage)
        {
            var package = await _context.CompensationPackages
                .Include(p => p.Advantages)
                .FirstOrDefaultAsync(p => p.Id == packageId);

            if (package == null) throw new Exception("Package not found");
            package.Advantages ??= new List<EmployeeAdvantage>();
            package.Advantages.Add(advantage);
            await _context.SaveChangesAsync();
        }
    }
}
