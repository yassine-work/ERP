using ERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Services
{
    public interface IAvantageService
    {
        Task<List<Avantage>> GetAllAvantagesAsync();
        Task<Avantage> GetAvantageByIdAsync(int id);
        Task<Avantage> CreateAvantageAsync(Avantage avantage);
        Task UpdateAvantageAsync(Avantage avantage);
        Task DeleteAvantageAsync(int id);
        Task<List<Avantage>> GetAvantagesByTypeAsync(string type);
        Task<List<Avantage>> GetAvantagesActifsAsync();
        Task AttribuerAvantageEmployeAsync(int employeId, int avantageId, System.DateTime? dateExpiration = null, string conditions = null);
        Task RetirerAvantageEmployeAsync(int employeAvantageId);
        Task<List<EmployeAvantage>> GetAvantagesByEmployeIdAsync(int employeId);
        Task<decimal> CalculerCoutTotalAvantagesAsync(int employeId);
    }
}