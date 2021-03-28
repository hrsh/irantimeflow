using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Pages
{
    public class IndexPage : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
