using ERP.Models;


namespace GestionRH.Services
{
    public interface IBulletinPaieService
    {
        Task<BulletinPaie> GenererBulletinPaieAsync(int salaireId);
        Task<List<BulletinPaie>> GetBulletinsByEmployeIdAsync(int employeId);
        Task<List<BulletinPaie>> GetBulletinsByPeriodeAsync(int mois, int annee);
        Task<BulletinPaie> GetBulletinByIdAsync(int id);
        Task<byte[]> GenererPDFBulletinAsync(int bulletinId);
        Task MarquerCommePayeAsync(int bulletinId, string referenceTransaction = null);
    }
}