using Microsoft.EntityFrameworkCore;
using ERP.Models;

namespace ERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        // You will add your tables here later
        public DbSet<ERP.Models.Employe> Employes { get; set; }
    }
}