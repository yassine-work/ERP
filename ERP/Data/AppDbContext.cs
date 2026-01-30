using Microsoft.EntityFrameworkCore;
using ERP.Models;

namespace ERP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        // You will add your tables here later
        public DbSet<ERP.Models.Employe> Employes { get; set; }
        
        // Compensation system
        public DbSet<CompensationPackage> CompensationPackages { get; set; }

        // Components
        public DbSet<EmployeeAdvantage> EmployeeAdvantages { get; set; }
        public DbSet<EmployeeAllowance> EmployeeAllowances { get; set; }
        public DbSet<EmployeeBonus> EmployeeBonuses { get; set; }

        // Types (catalog tables)
        public DbSet<AdvantageType> AdvantageTypes { get; set; }
        public DbSet<AllowanceType> AllowanceTypes { get; set; }
        public DbSet<BonusType> BonusTypes { get; set; }

        public DbSet<Poste> Postes { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employe -> CompensationPackages (1..n)
            modelBuilder.Entity<CompensationPackage>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.CompensationPackages)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // CompensationPackage -> Advantages
            modelBuilder.Entity<EmployeeAdvantage>()
                .HasOne(a => a.CompensationPackage)
                .WithMany(p => p.Advantages)
                .HasForeignKey(a => a.CompensationPackageId);

            // CompensationPackage -> Allowances
            modelBuilder.Entity<EmployeeAllowance>()
                .HasOne(a => a.CompensationPackage)
                .WithMany(p => p.Allowances)
                .HasForeignKey(a => a.CompensationPackageId);

            // CompensationPackage -> Bonuses
            modelBuilder.Entity<EmployeeBonus>()
                .HasOne(b => b.CompensationPackage)
                .WithMany(p => p.Bonuses)
                .HasForeignKey(b => b.CompensationPackageId);

            // Type relations
            modelBuilder.Entity<EmployeeAdvantage>()
                .HasOne(a => a.AdvantageType)
                .WithMany()
                .HasForeignKey(a => a.AdvantageTypeId);

            modelBuilder.Entity<EmployeeAllowance>()
                .HasOne(a => a.AllowanceType)
                .WithMany()
                .HasForeignKey(a => a.AllowanceTypeId);

            modelBuilder.Entity<EmployeeBonus>()
                .HasOne(b => b.BonusType)
                .WithMany()
                .HasForeignKey(b => b.BonusTypeId);

            // Uniqueness rules (clean data, no duplicates per package)
            modelBuilder.Entity<EmployeeAdvantage>()
                .HasIndex(a => new { a.CompensationPackageId, a.AdvantageTypeId })
                .IsUnique();

            modelBuilder.Entity<EmployeeAllowance>()
                .HasIndex(a => new { a.CompensationPackageId, a.AllowanceTypeId })
                .IsUnique();

            modelBuilder.Entity<EmployeeBonus>()
                .HasIndex(b => new { b.CompensationPackageId, b.BonusTypeId })
                .IsUnique();

            modelBuilder.Entity<Employe>()
                .HasOne(e => e.Poste)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PosteId)
                .OnDelete(DeleteBehavior.Restrict); 
            
            DataSeeder.SeedPostes(modelBuilder);
            DataSeeder.SeedAllowanceTypes(modelBuilder);
            DataSeeder.SeedBonusTypes(modelBuilder);
            DataSeeder.SeedAdvantageTypes(modelBuilder);




        }
    }
}