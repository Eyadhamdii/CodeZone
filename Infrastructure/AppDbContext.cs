using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Stock> Stock { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
            .HasOne(s => s.Store)
                .WithMany()
                .HasForeignKey(s => s.StoreId);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Item)
                .WithMany()
                .HasForeignKey(s => s.ItemId);
        }
    }
}