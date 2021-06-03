using Irantimeline.Models;
using Irantimeline.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Irantimeline.Areas.Timeline.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public int PageIndex { get; set; }

        public IndexModel(
            IMediator mediator,
            UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public IEnumerable<TimelineViewModel> TimelineList { get; set; }

        public async Task<IActionResult> OnGetAsync(
            int? pageIndex,
            CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return BadRequest();

            if (pageIndex.HasValue)
            {
                if (pageIndex <= 1) pageIndex = 1;
                PageIndex = pageIndex.Value;
            }

            var query = new GetMyTimelineQuery(user.Email, pageIndex ?? 1);
            TimelineList = await _mediator.Send(query, ct);

            return Page();
        }
    }
}
