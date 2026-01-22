
using ERP.Models;
using ERP.Services;
using ERP.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionRH.Services
{
    public class BulletinPaieService : IBulletinPaieService
    {
        private readonly AppDbContext _context;
        private readonly IAvantageService _avantageService;

        public BulletinPaieService(AppDbContext context)
        {
            _context = context;
            _avantageService = new AvantageService(context);
        }

        public async Task<BulletinPaie> GenererBulletinPaieAsync(int salaireId)
        {
            var salaire = await _context.Salaires
                .Include(s => s.Employe)
                .FirstOrDefaultAsync(s => s.Id == salaireId);
                
            if (salaire == null)
                throw new ArgumentException("Salaire non trouvé");

            var existingBulletin = await _context.BulletinPaies
                .FirstOrDefaultAsync(bp => bp.SalaireId == salaireId);
                
            if (existingBulletin != null)
                return existingBulletin;

            var coutAvantages = await _avantageService.CalculerCoutTotalAvantagesAsync(salaire.EmployeId);

            string numeroBulletin = $"BP-{salaire.EmployeId}-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            var bulletin = new BulletinPaie
            {
                SalaireId = salaireId,
                NumeroBulletin = numeroBulletin,
                Mois = salaire.DateDebut.Month,
                Annee = salaire.DateDebut.Year,
                DatePaiement = DateTime.Now,
                TotalAvantages = coutAvantages,
                TotalRetenues = (salaire.CotisationsSociales ?? 0) + (salaire.ImpotRevenu ?? 0) + (salaire.AutresRetenues ?? 0),
                NetAPayer = salaire.SalaireNet,
                ModePaiement = "Virement",
                EstPaye = false,
                DateCreation = DateTime.Now
            };

            _context.BulletinPaies.Add(bulletin);
            await _context.SaveChangesAsync();

            return bulletin;
        }

        public async Task<List<BulletinPaie>> GetBulletinsByEmployeIdAsync(int employeId)
        {
            return await _context.BulletinPaies
                .Include(bp => bp.Salaire)
                .Where(bp => bp.Salaire.EmployeId == employeId)
                .OrderByDescending(bp => bp.Annee)
                .ThenByDescending(bp => bp.Mois)
                .ToListAsync();
        }

        public async Task<List<BulletinPaie>> GetBulletinsByPeriodeAsync(int mois, int annee)
        {
            return await _context.BulletinPaies
                .Include(bp => bp.Salaire)
                .Include(bp => bp.Salaire.Employe)
                .Where(bp => bp.Mois == mois && bp.Annee == annee)
                .OrderBy(bp => bp.Salaire.Employe.Nom)
                .ThenBy(bp => bp.Salaire.Employe.Prenom)
                .ToListAsync();
        }

        public async Task<BulletinPaie> GetBulletinByIdAsync(int id)
        {
            return await _context.BulletinPaies
                .Include(bp => bp.Salaire)
                .Include(bp => bp.Salaire.Employe)
                .FirstOrDefaultAsync(bp => bp.Id == id);
        }

        public async Task<byte[]> GenererPDFBulletinAsync(int bulletinId)
        {
            var bulletin = await GetBulletinByIdAsync(bulletinId);
            if (bulletin == null)
                throw new ArgumentException("Bulletin non trouvé");

            // Logique de génération PDF (à implémenter)
            return new byte[0];
        }

        public async Task MarquerCommePayeAsync(int bulletinId, string referenceTransaction = null)
        {
            var bulletin = await _context.BulletinPaies.FindAsync(bulletinId);
            if (bulletin != null)
            {
                bulletin.EstPaye = true;
                bulletin.DatePaiementEffectif = DateTime.Now;
                bulletin.ReferenceTransaction = referenceTransaction;
                
                _context.Entry(bulletin).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}