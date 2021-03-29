using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IranTimeFlow.WebApp.Areas.Admin.Pages
{
    public class AddTimelinePage : PageModel
    {
        private readonly IMediator _mediator;

        public AddTimelinePage(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult OnGet() => Page();
    }
}
