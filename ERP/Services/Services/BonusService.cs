using ERP.Models;
using ERP.Data;
using System.Collections.Generic;
using ERP.Services.IServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services
{
    public class BonusService : IBonusService
    {
        private readonly AppDbContext _context;

        public BonusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeBonus>> GetAllBonusesAsync()
        {
            return await _context.EmployeeBonuses.Include(p => p.BonusType).ToListAsync();
        }

        public async Task<EmployeeBonus> GetBonusByIdAsync(int id)
        {
            var bonus = await _context.EmployeeBonuses.Include(p => p.BonusType).FirstOrDefaultAsync(x => x.EmployeeBonusId == id);
            if (bonus == null)
            {
                throw new Exception($"EmployeeBonus {id} not found");
            }
            return bonus;
        }

        public async Task<EmployeeBonus> SaveBonusAsync(EmployeeBonus bonus)
        {
            _context.EmployeeBonuses.Add(bonus);
            await _context.SaveChangesAsync();
            return bonus;
        }

        public async Task<EmployeeBonus> UpdateBonusAsync(int id, EmployeeBonus bonus)
        {
            var existingBonus = await _context.EmployeeBonuses.FindAsync(id);
            if (existingBonus == null) return null;

            existingBonus.Amount = bonus.Amount;
            existingBonus.BonusTypeId = bonus.BonusTypeId;
            existingBonus.CompensationPackageId = bonus.CompensationPackageId;

            await _context.SaveChangesAsync();
            return existingBonus;
        }

        public async Task<bool> DeleteBonusAsync(int id)
        {
            var bonus = await _context.EmployeeBonuses.FindAsync(id);
            if (bonus == null) return false;

            _context.EmployeeBonuses.Remove(bonus);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
