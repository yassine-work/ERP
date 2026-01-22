using ERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionRH.Services
{
    public interface ISalaireService
    {
        Task<List<Salaire>> GetAllSalairesAsync();
        Task<Salaire> GetSalaireByIdAsync(int id);
        Task<List<Salaire>> GetSalairesByEmployeIdAsync(int employeId);
        Task<Salaire> CreateSalaireAsync(Salaire salaire);
        Task UpdateSalaireAsync(Salaire salaire);
        Task DeleteSalaireAsync(int id);
        Task<decimal> CalculerSalaireNetAsync(decimal brut, decimal cotisations, decimal impot, decimal autresRetenues);
        Task<List<Salaire>> GetSalairesByPeriodeAsync(DateTime dateDebut, DateTime dateFin);
        Task<Dictionary<string, decimal>> GetStatistiquesSalairesAsync();
        Task<Salaire> GetSalaireActuelAsync(int employeId);
    }
}