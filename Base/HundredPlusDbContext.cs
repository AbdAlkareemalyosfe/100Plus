using Base.Models;
using Microsoft.EntityFrameworkCore;


namespace Base
{
    public class HundredPlusDbContext : DbContext
    {
        public HundredPlusDbContext(DbContextOptions<HundredPlusDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasOne(p => p.Product).WithMany(c => c.Categories).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .HasOne(o => o.Order).WithMany(c => c.Categories).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .HasOne(o => o.Offer).WithMany(c => c.Categories);
            modelBuilder.Entity<User>()
                .HasMany(o => o.orders).WithOne(u => u.user).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
              .HasMany(o => o.Offers).WithOne(p => p.product).OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Category> Categores { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

    }
}