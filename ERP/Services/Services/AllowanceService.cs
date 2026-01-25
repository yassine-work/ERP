using ERP.Data;
using ERP.Models;
using ERP.Services.IServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services
{
    public class AllowanceService : IAllowanceService
    {
        private readonly AppDbContext _context;

        public AllowanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeAllowance> SaveAllowanceAsync(EmployeeAllowance allowance)
        {
            _context.EmployeeAllowances.Add(allowance);
            await _context.SaveChangesAsync();
            return allowance;
        }

        public async Task<EmployeeAllowance> GetAllowanceByIdAsync(int id)
        {
            var allowance = await _context.EmployeeAllowances.Include(p => p.AllowanceType).FirstOrDefaultAsync(x => x.EmployeeAllowanceId == id);
            if (allowance == null)
            {
                throw new Exception($"EmployeeAllowance {id} not found");
            }
            return allowance;
        }

        public async Task<IEnumerable<EmployeeAllowance>> GetAllAllowancesAsync()
        {
            return await _context.EmployeeAllowances.Include(p => p.AllowanceType).ToListAsync();
        }

        public async Task<EmployeeAllowance> UpdateAllowanceAsync(EmployeeAllowance allowance)
        {
            _context.EmployeeAllowances.Update(allowance);
            await _context.SaveChangesAsync();
            return allowance;
        }

        public async Task<bool> DeleteAllowanceAsync(int id)
        {
            var allowance = await _context.EmployeeAllowances.FindAsync(id);
            if (allowance == null)
                return false;

            _context.EmployeeAllowances.Remove(allowance);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}