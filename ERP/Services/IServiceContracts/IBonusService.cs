using ERP.Data;
using ERP.Models;
using System.Collections.Generic;

namespace ERP.Services.IServiceContracts
{
    public interface IBonusService
    {
        Task<IEnumerable<EmployeeBonus>> GetAllBonusesAsync();
        Task<EmployeeBonus> GetBonusByIdAsync(int id);
        Task<EmployeeBonus> SaveBonusAsync(EmployeeBonus bonus);
        Task<EmployeeBonus> UpdateBonusAsync(int id, EmployeeBonus bonus);
        Task<bool> DeleteBonusAsync(int id);
    }
}