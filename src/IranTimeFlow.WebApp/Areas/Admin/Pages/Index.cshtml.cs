using IranTimeFlow.WebApp.PagedModel;
using IranTimeFlow.WebApp.Queries;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Areas.Admin.Pages
{
    public class IndexPage : PageModel
    {
        private readonly IMediator _mediator;

        public IndexPage(IMediator mediator)
        {
            _mediator = mediator;
        }

        public PagedList<TimelineViewModel> TimelineList { get; set; }

        public async Task<IActionResult> OnGetAsync(
            int? pageIndex,
            CancellationToken ct)
        {
            var query = new GetLatestQuery(pageIndex ?? 1, a => a.Id > 0);
            TimelineList = await _mediator.Send(query, ct);
            return Page();
        }
    }
}
