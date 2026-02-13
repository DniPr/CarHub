using CarHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarHub.Data
{
    public class CarHubDbContext : IdentityDbContext
    {
        public CarHubDbContext(DbContextOptions<CarHubDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CarAd> CarAds { get; set; } = null!;
        public virtual DbSet<Favorite> FavoriteCarAds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.CarAdId });

            builder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorite>()
                .HasOne(f => f.CarAd)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.CarAdId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sedan" },
                new Category { Id = 2, Name = "Hatchback" },
                new Category { Id = 3, Name = "SUV" },
                new Category { Id = 4, Name = "Coupe" },
                new Category { Id = 5, Name = "Wagon" },
                new Category { Id = 6, Name = "Pickup" });
        }
    }
}
