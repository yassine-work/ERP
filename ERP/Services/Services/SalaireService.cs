
using ERP.Models;
using System;
using System.Collections.Generic;
using ERP.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRH.Services
{
    public class SalaireService : ISalaireService
    {
        private readonly AppDbContext _context;

        public SalaireService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Salaire>> GetAllSalairesAsync()
        {
            return await _context.Salaires
                .Include(s => s.Employe)
                .OrderByDescending(s => s.DateCreation)
                .ToListAsync();
        }

        public async Task<Salaire> GetSalaireByIdAsync(int id)
        {
            return await _context.Salaires
                .Include(s => s.Employe)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Salaire>> GetSalairesByEmployeIdAsync(int employeId)
        {
            return await _context.Salaires
                .Where(s => s.EmployeId == employeId)
                .OrderByDescending(s => s.DateDebut)
                .ToListAsync();
        }

        public async Task<Salaire> CreateSalaireAsync(Salaire salaire)
        {
            // Calculer le salaire brut
            salaire.SalaireBrut = CalculerSalaireBrut(salaire);
            
            // Calculer le salaire net
            salaire.SalaireNet = CalculerSalaireNet(
                salaire.SalaireBrut,
                salaire.CotisationsSociales ?? 0,
                salaire.ImpotRevenu ?? 0,
                salaire.AutresRetenues ?? 0);

            salaire.DateCreation = DateTime.Now;
            
            _context.Salaires.Add(salaire);
            await _context.SaveChangesAsync();
            
            return salaire;
        }

        public async Task UpdateSalaireAsync(Salaire salaire)
        {
            salaire.SalaireBrut = CalculerSalaireBrut(salaire);
            salaire.SalaireNet = CalculerSalaireNet(
                salaire.SalaireBrut,
                salaire.CotisationsSociales ?? 0,
                salaire.ImpotRevenu ?? 0,
                salaire.AutresRetenues ?? 0);
                
            salaire.DateModification = DateTime.Now;
            
            _context.Entry(salaire).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalaireAsync(int id)
        {
            var salaire = await _context.Salaires.FindAsync(id);
            if (salaire != null)
            {
                _context.Salaires.Remove(salaire);
                await _context.SaveChangesAsync();
            }
        }

        public Task<decimal> CalculerSalaireNetAsync(decimal brut, decimal cotisations, decimal impot, decimal autresRetenues)
        {
            decimal totalRetenues = cotisations + impot + autresRetenues;
            decimal net = Math.Max(0, brut - totalRetenues);
            return Task.FromResult(net);
        }

        private decimal CalculerSalaireBrut(Salaire salaire)
        {
            decimal brut = salaire.SalaireBase;
            brut += salaire.PrimePerformance ?? 0;
            brut += salaire.PrimeAnciennete ?? 0;
            brut += salaire.AutresPrimes ?? 0;
            
            if (salaire.HeuresSupplementaires.HasValue && salaire.TauxHeureSup.HasValue)
            {
                brut += salaire.HeuresSupplementaires.Value * salaire.TauxHeureSup.Value;
            }
            
            return brut;
        }

        private decimal CalculerSalaireNet(decimal brut, decimal cotisations, decimal impot, decimal autresRetenues)
        {
            decimal totalRetenues = cotisations + impot + autresRetenues;
            return Math.Max(0, brut - totalRetenues);
        }

        public async Task<List<Salaire>> GetSalairesByPeriodeAsync(DateTime dateDebut, DateTime dateFin)
        {
            return await _context.Salaires
                .Where(s => s.DateDebut >= dateDebut && s.DateDebut <= dateFin)
                .Include(s => s.Employe)
                .OrderBy(s => s.DateDebut)
                .ToListAsync();
        }

        public async Task<Dictionary<string, decimal>> GetStatistiquesSalairesAsync()
        {
            var salaires = await _context.Salaires.ToListAsync();
            
            return new Dictionary<string, decimal>
            {
                { "MoyenneSalaireBrut", salaires.Any() ? salaires.Average(s => s.SalaireBrut) : 0 },
                { "MoyenneSalaireNet", salaires.Any() ? salaires.Average(s => s.SalaireNet) : 0 },
                { "TotalMasseSalariale", salaires.Any() ? salaires.Sum(s => s.SalaireBrut) : 0 },
                { "SalaireMax", salaires.Any() ? salaires.Max(s => s.SalaireBrut) : 0 },
                { "SalaireMin", salaires.Any() ? salaires.Min(s => s.SalaireBrut) : 0 }
            };
        }

        public async Task<Salaire> GetSalaireActuelAsync(int employeId)
        {
            return await _context.Salaires
                .Where(s => s.EmployeId == employeId && s.DateFin == null)
                .OrderByDescending(s => s.DateDebut)
                .FirstOrDefaultAsync();
        }
    }
}