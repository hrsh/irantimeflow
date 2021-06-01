using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Pages
{
    public class LoginPage : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginPage(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public class LoginModel
        {
            [Display(Name = "ایمیل")]
            [Required(ErrorMessage = "مقدار معتبری برای ایمیل وارد کنید")]
            public string Email { get; set; }

            [Display(Name = "کلمه عبور")]
            [Required(ErrorMessage = "مقدار معتبری برای کلمه عبور وارد کنید")]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string Message { get; set; }

        [BindProperty]
        public LoginModel InputModel { get; set; }

        public string ReturnUrl { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var user = await _userManager.FindByEmailAsync(InputModel.Email);
            if (user is null)
            {
                Message = "داده‌های ارسال شده معتبر نیستند!";
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName: user.UserName,
                password: InputModel.Password,
                isPersistent: InputModel.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded) return LocalRedirect(returnUrl);

            Message = "داده‌های ارسال شده معتبر نیستند!";
            return Page();
        }
    }
}
