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
    }
}
