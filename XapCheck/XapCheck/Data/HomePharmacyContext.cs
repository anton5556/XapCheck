using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using XapCheck.Models;

namespace XapCheck.Data
{
    public class HomePharmacyContext : DbContext
    {
        public HomePharmacyContext()
        {
        }

        public HomePharmacyContext(DbContextOptions<HomePharmacyContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<PurchaseListItem> PurchaseList { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["HomePharmacyDb"];                
                if (connectionString != null && !string.IsNullOrWhiteSpace(connectionString.ConnectionString))
                {
                    optionsBuilder.UseSqlServer(connectionString.ConnectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.Medicines)
                .WithOne(m => m.UserProfile)
                .HasForeignKey(m => m.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ActionLog>()
                .HasOne(l => l.UserProfile)
                .WithMany()
                .HasForeignKey(l => l.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActionLog>()
                .HasOne(l => l.Medicine)
                .WithMany()
                .HasForeignKey(l => l.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseListItem>()
                .HasOne(p => p.Medicine)
                .WithMany()
                .HasForeignKey(p => p.MedicineId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseListItem>()
                .HasOne(p => p.UserProfile)
                .WithMany()
                .HasForeignKey(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppSetting>()
                .HasOne(s => s.UserProfile)
                .WithMany()
                .HasForeignKey(s => s.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
