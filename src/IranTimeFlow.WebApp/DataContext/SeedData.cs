using IranTimeFlow.WebApp.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.DataContext
{
    public class SeedData
    {
        public static async Task SeedDefaultUser(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var user = new IdentityUser
            {
                Email = "hrshojaie@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "09173148953",
                AccessFailedCount = 0,
                LockoutEnabled = true,
                UserName = "hrshojaie",
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(user, "Mazdak@12345");

            var roles = ClassProperties
                .GetFields(typeof(RoleNames))
                .Resolve(a => a.Key);

            foreach(var role in roles)
            {
                var r = await roleManager.FindByNameAsync(role);
                if(r is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            await userManager.AddToRoleAsync(user, RoleNames.Admin);
        }
    }
}
