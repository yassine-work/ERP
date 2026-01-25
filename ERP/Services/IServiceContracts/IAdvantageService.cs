using ERP.Models;
using System.Collections.Generic;
using ERP.Data;



namespace ERP.Services.IServiceContracts
{
    public interface IAdvantageService
    {
        Task<EmployeeAdvantage> SaveAdvantageAsync(EmployeeAdvantage advantage);
        Task<EmployeeAdvantage> GetAdvantageByIdAsync(int id);
        Task<IEnumerable<EmployeeAdvantage>> GetAllAdvantagesAsync();
        Task<EmployeeAdvantage> UpdateAdvantageAsync(EmployeeAdvantage advantage);
        Task<bool> DeleteAdvantageAsync(int id);
    }
}