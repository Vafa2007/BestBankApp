using BestBankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BestBankApp.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() {}

        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public virtual DbSet<Clients> CLIENTS { get; set; }
        public virtual DbSet<Credits> CREDIT_APPLY { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BestBankApp;Trusted_Connection=True;");
            }
        }
    }
}
