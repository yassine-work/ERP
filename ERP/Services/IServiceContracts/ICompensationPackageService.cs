using ERP.Models;

namespace ERP.Services.IServiceContracts
{
    public interface ICompensationPackageService
    {
        // ════════════════════════════════════════════════════════════
        // BASIC CRUD
        // ════════════════════════════════════════════════════════════
        Task<CompensationPackage> SaveCompensationPackageAsync(CompensationPackage package);
        Task<CompensationPackage> GetCompensationPackageByIdAsync(int id);
        Task<IEnumerable<CompensationPackage>> GetAllCompensationPackagesAsync();
        Task<CompensationPackage> UpdateCompensationPackageAsync(CompensationPackage package);
        Task<bool> DeleteCompensationPackageAsync(int id);

        // ════════════════════════════════════════════════════════════
        // PACKAGE LIFECYCLE (VERSIONING)
        // ════════════════════════════════════════════════════════════
        
        /// <summary>
        /// Get the currently active package for an employee
        /// Returns null if no active package exists
        /// </summary>
        Task<CompensationPackage?> GetActivePackageForEmployeeAsync(int employeeId);
        
        /// <summary>
        /// Activate a specific package
        /// BUSINESS RULE: Only one package can be active per employee
        /// Automatically deactivates other packages
        /// </summary>
        Task ActivatePackageAsync(int packageId);
        
        /// <summary>
        /// Deactivate a package (archive it)
        /// Sets IsActive = false, EffectiveTo = DateTime.Now
        /// Part of versioning flow
        /// </summary>
        Task DeactivatePackageAsync(int packageId);

        /// <summary>
        /// Get all packages for an employee (history)
        /// Ordered by EffectiveFrom descending
        /// </summary>
        Task<IEnumerable<CompensationPackage>> GetPackageHistoryAsync(int employeeId);

        // ════════════════════════════════════════════════════════════
        // COMPONENT MANAGEMENT
        // ════════════════════════════════════════════════════════════
        
        Task AddBonusAsync(int packageId, EmployeeBonus bonus);
        Task AddAllowanceAsync(int packageId, EmployeeAllowance allowance);
        Task AddAdvantageAsync(int packageId, EmployeeAdvantage advantage);

        Task RemoveBonusAsync(int bonusId);
        Task RemoveAllowanceAsync(int allowanceId);
        Task RemoveAdvantageAsync(int advantageId);

        // ════════════════════════════════════════════════════════════
        // BUSINESS OPERATIONS
        // ════════════════════════════════════════════════════════════
        
        /// <summary>
        /// Calculate total compensation for a package
        /// Base + Allowances + Bonuses
        /// </summary>
        Task<decimal> CalculateTotalCompensationAsync(int packageId);

        /// <summary>
        /// Validate package against business rules
        /// - Base salary >= poste minimum
        /// - No duplicate component types
        /// - Dates are consistent
        /// </summary>
        Task<(bool IsValid, List<string> Errors)> ValidatePackageAsync(CompensationPackage package);

        /// <summary>
        /// Create a new package version from existing
        /// Core versioning operation
        /// Deactivates old, creates new, preserves audit trail
        /// </summary>
        Task<CompensationPackage> CreatePackageVersionAsync(
            int employeeId,
            decimal newBaseSalary,
            DateTime effectiveFrom,
            List<EmployeeAllowance>? allowances = null,
            List<EmployeeBonus>? bonuses = null,
            List<EmployeeAdvantage>? advantages = null);
    }
}