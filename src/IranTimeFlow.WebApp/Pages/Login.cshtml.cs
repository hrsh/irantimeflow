using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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

        public string Message { get; set; }

        [BindProperty]
        public LoginModel InputModel { get; set; }

        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToPage("./Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByEmailAsync(InputModel.Email);
            if (user is null)
            {
                Message = "داده‌های ارسال شده معتبر نیستند!";
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, 
                InputModel.Password, 
                InputModel.RememberMe, 
                true);

            if (result.Succeeded) return RedirectToPage("Index");

            Message = "داده‌های ارسال شده معتبر نیستند!";
            return Page();
        }
    }
}
