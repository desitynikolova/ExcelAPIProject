using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Models.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Date
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<LastReadedFile> LastReadedFiles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RN4FUO9\SQLEXPRESS;Database=ExcelDb;Trusted_Connection=True");
        }

        public async Task<int> SaveChangesAsync()
        {
            var entries = this.ChangeTracker.Entries().Where(x => x.Entity is IAuditInfo && x.State == EntityState.Added).ToList();

            foreach (EntityEntry entry in entries)
            {
                var entity = (IAuditInfo)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = DateTime.Now;
                        break;
                }
            }
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}