using ApartmentCrawler.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApartmentCrawler.DbProvider
{
    public class AptCrawlerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Filename=apartmentcrawler.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(k => k.UserId);
            });
        }      
    }
}
