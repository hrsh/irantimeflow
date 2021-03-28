using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.DataContext
{
    public class SeedData
    {
        public static async Task SeedDefaultUser(UserManager<IdentityUser> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var user = new IdentityUser
            {
                Email = "user@email.com",
                EmailConfirmed = true,
                PhoneNumber = "09000000000",
                AccessFailedCount = 0,
                LockoutEnabled = true,
                UserName = "hrshojaie",
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(user, "T#mpPa$$w0rd");
        }
    }
}
