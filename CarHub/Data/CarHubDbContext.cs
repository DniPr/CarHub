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
        }
    }
}
