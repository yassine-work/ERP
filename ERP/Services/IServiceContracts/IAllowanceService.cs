using ERP.Models;
using System.Collections.Generic;

namespace  ERP.Services.IServiceContracts
{
    public interface IAllowanceService
    {
        Task<EmployeeAllowance> SaveAllowanceAsync(EmployeeAllowance allowance);
        Task<EmployeeAllowance> GetAllowanceByIdAsync(int id);
        Task<IEnumerable<EmployeeAllowance>> GetAllAllowancesAsync();
        Task<EmployeeAllowance> UpdateAllowanceAsync(EmployeeAllowance allowance);
        Task<bool> DeleteAllowanceAsync(int id);
    }
}