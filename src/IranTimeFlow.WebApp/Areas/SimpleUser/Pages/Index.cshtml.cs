using IranTimeFlow.WebApp.Helpers;
using IranTimeFlow.WebApp.PagedModel;
using IranTimeFlow.WebApp.Queries;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Areas.SimpleUser.Pages
{
    public class IndexPage : PageModel
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexPage(
            IMediator mediator,
            UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public PagedList<TimelineViewModel> TimelineList { get; set; }

        public async Task<IActionResult> OnGetAsync(
            int? pageIndex,
            CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return BadRequest();
            if (await _userManager.IsInRoleAsync(user, RoleNames.Admin))
                return RedirectToPage("Index", new { area = AreaNames.Admin });
            var query = new GetMyTimelineQuery(user.Email, pageIndex ?? 1);
            TimelineList = await _mediator.Send(query, ct);

            return Page();
        }
    }
}
