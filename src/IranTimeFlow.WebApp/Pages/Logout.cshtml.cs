using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Pages
{
    public class LogoutPage : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutPage(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("Index");
        }
    }
}
