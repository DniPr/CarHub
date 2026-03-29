using Microsoft.AspNetCore.Identity;

namespace CarHub.Common
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            RoleManager<IdentityRole> roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await SeedRolesAsync(roleManager);
            await SeedAdminAsync(userManager);
        }
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                ApplicationRoles.User,
                ApplicationRoles.Administrator
            };

            foreach (string role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdminAsync(UserManager<IdentityUser> userManager)
        {
            const string adminEmail = "admin@carhub.bg";
            const string adminPassword = "Admin12345";
            IdentityUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                IdentityResult createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Admin user could not be created: {string.Join("; ", createResult.Errors.Select(e => e.Description))}");
                }
            }
            bool isAdmin = await userManager.IsInRoleAsync(adminUser, ApplicationRoles.Administrator);
            if (!isAdmin)
            {
                await userManager.AddToRoleAsync(adminUser, ApplicationRoles.Administrator);
            }
        }
    }
}