using ERP.Models;
using System.Collections.Generic;

namespace ERP.Services.IServiceContracts
{
    public interface ICompensationPackageService
    {
        Task<CompensationPackage> SaveCompensationPackageAsync(CompensationPackage package);
        Task<CompensationPackage> GetCompensationPackageByIdAsync(int id);
        Task<IEnumerable<CompensationPackage>> GetAllCompensationPackagesAsync();
        Task<CompensationPackage> UpdateCompensationPackageAsync(CompensationPackage package);
        Task<bool> DeleteCompensationPackageAsync(int id);

        Task<CompensationPackage?> GetActivePackageForEmployeeAsync(int employeeId);
        Task ActivatePackageAsync(int packageId);
        Task DeactivatePackageAsync(int packageId);

        Task AddBonusAsync(int packageId, EmployeeBonus bonus);
        Task AddAllowanceAsync(int packageId, EmployeeAllowance allowance);
        Task AddAdvantageAsync(int packageId, EmployeeAdvantage advantage);

        
    }
}