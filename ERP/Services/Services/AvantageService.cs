using ERP.Models;
using ERP.Services;
using ERP.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionRH.Services
{
    public class AvantageService : IAvantageService
    {
        private readonly AppDbContext _context;

        public AvantageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Avantage>> GetAllAvantagesAsync()
        {
            return await _context.Avantages
                .OrderBy(a => a.TypeAvantage)
                .ThenBy(a => a.Libelle)
                .ToListAsync();
        }

        public async Task<Avantage> GetAvantageByIdAsync(int id)
        {
            return await _context.Avantages.FindAsync(id);
        }

        public async Task<Avantage> CreateAvantageAsync(Avantage avantage)
        {
            if (avantage.MontantTotal == null && (avantage.MontantEmploye.HasValue || avantage.MontantEmployeur.HasValue))
            {
                avantage.MontantTotal = (avantage.MontantEmploye ?? 0) + (avantage.MontantEmployeur ?? 0);
            }

            avantage.DateCreation = DateTime.Now;
            avantage.Statut = "Actif";
            
            _context.Avantages.Add(avantage);
            await _context.SaveChangesAsync();
            
            return avantage;
        }

        public async Task UpdateAvantageAsync(Avantage avantage)
        {
            avantage.DateModification = DateTime.Now;
            _context.Entry(avantage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAvantageAsync(int id)
        {
            var avantage = await _context.Avantages.FindAsync(id);
            if (avantage != null)
            {
                var utilisations = await _context.EmployeAvantages
                    .Where(ea => ea.AvantageId == id && ea.Statut == "Actif")
                    .CountAsync();
                    
                if (utilisations > 0)
                {
                    throw new InvalidOperationException("Impossible de supprimer un avantage attribué à des employés.");
                }
                
                _context.Avantages.Remove(avantage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Avantage>> GetAvantagesByTypeAsync(string type)
        {
            return await _context.Avantages
                .Where(a => a.TypeAvantage == type && a.Statut == "Actif")
                .ToListAsync();
        }

        public async Task<List<Avantage>> GetAvantagesActifsAsync()
        {
            return await _context.Avantages
                .Where(a => a.Statut == "Actif")
                .ToListAsync();
        }

        public async Task AttribuerAvantageEmployeAsync(int employeId, int avantageId, DateTime? dateExpiration = null, string conditions = null)
        {
            var employe = await _context.Employes.FindAsync(employeId);
            if (employe == null)
                throw new ArgumentException("Employé non trouvé");

            var avantage = await _context.Avantages.FindAsync(avantageId);
            if (avantage == null || avantage.Statut != "Actif")
                throw new ArgumentException("Avantage non disponible");

            var existe = await _context.EmployeAvantages
                .AnyAsync(ea => ea.EmployeId == employeId && ea.AvantageId == avantageId && ea.Statut == "Actif");
                
            if (existe)
                throw new InvalidOperationException("Cet avantage est déjà attribué à cet employé");

            var employeAvantage = new EmployeAvantage
            {
                EmployeId = employeId,
                AvantageId = avantageId,
                DateAttribution = DateTime.Now,
                DateExpiration = dateExpiration,
                Conditions = conditions,
                Statut = "Actif"
            };

            _context.EmployeAvantages.Add(employeAvantage);
            await _context.SaveChangesAsync();
        }

        public async Task RetirerAvantageEmployeAsync(int employeAvantageId)
        {
            var employeAvantage = await _context.EmployeAvantages.FindAsync(employeAvantageId);
            if (employeAvantage != null)
            {
                employeAvantage.Statut = "Expiré";
                employeAvantage.DateExpiration = DateTime.Now;
                _context.Entry(employeAvantage).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<EmployeAvantage>> GetAvantagesByEmployeIdAsync(int employeId)
        {
            return await _context.EmployeAvantages
                .Where(ea => ea.EmployeId == employeId)
                .Include(ea => ea.Avantage)
                .OrderByDescending(ea => ea.DateAttribution)
                .ToListAsync();
        }

        public async Task<decimal> CalculerCoutTotalAvantagesAsync(int employeId)
        {
            var avantages = await _context.EmployeAvantages
                .Where(ea => ea.EmployeId == employeId && ea.Statut == "Actif")
                .Include(ea => ea.Avantage)
                .ToListAsync();

            decimal coutTotal = 0;
            foreach (var ea in avantages)
            {
                coutTotal += ea.Avantage.MontantEmployeur ?? 0;
            }

            return coutTotal;
        }
    }
}