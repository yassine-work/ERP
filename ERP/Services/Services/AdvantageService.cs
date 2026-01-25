using ERP.Data;
using ERP.Models;
using ERP.Services.IServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services
{
    public class AdvantageService : IAdvantageService
    {
        private readonly AppDbContext _context;
        public AdvantageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeAdvantage> SaveAdvantageAsync(EmployeeAdvantage advantage)
        {
            _context.EmployeeAdvantages.Add(advantage);
            await _context.SaveChangesAsync();
            return advantage;
        }

        public async Task<EmployeeAdvantage> GetAdvantageByIdAsync(int id)
        {
            var advantage = await _context.EmployeeAdvantages.Include(p => p.AdvantageType).FirstOrDefaultAsync(x => x.EmployeeAdvantageId == id);
            if (advantage == null)
            {
                throw new Exception($"EmployeeAdvantage {id} not found");
            }
            return advantage;
        }

        public async Task<IEnumerable<EmployeeAdvantage>> GetAllAdvantagesAsync()
        {
            return await _context.EmployeeAdvantages.Include(p => p.AdvantageType).ToListAsync();
        }

        public async Task<EmployeeAdvantage> UpdateAdvantageAsync(EmployeeAdvantage advantage)
        {
            _context.EmployeeAdvantages.Update(advantage);
            await _context.SaveChangesAsync();
            return advantage;
        }

        public async Task<bool> DeleteAdvantageAsync(int id)
        {
            var advantage = await _context.EmployeeAdvantages.FindAsync(id);
            if (advantage == null)
            {
                return false;
            }
            _context.EmployeeAdvantages.Remove(advantage);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

