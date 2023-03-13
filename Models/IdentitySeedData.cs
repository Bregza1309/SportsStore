using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Pa$$w0rd";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            StoreIdentityContext db = app.ApplicationServices.CreateScope()
                                            .ServiceProvider.GetRequiredService<StoreIdentityContext>();
            if (db.Database.GetPendingMigrations().Any())
            {
                await db.Database.MigrateAsync();
            }
            UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope()
                                                        .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if(user == null)
            {
                user = new IdentityUser("Admin");
                user.Email = "admin@example.com";
                user.PhoneNumber = "073 123 4567";
                await userManager.CreateAsync(user,adminPassword);
            }
        }

    }
}
