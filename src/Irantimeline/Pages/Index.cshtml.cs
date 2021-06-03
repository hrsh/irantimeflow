using Irantimeline.Models;
using Irantimeline.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Irantimeline.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IEnumerable<TimelineViewModel> TimelineList { get; set; }

        public string Filter { get; set; }

        public string Search { get; set; }

        public int PageIndex { get; set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> OnGetAsync(
            int? pageIndex,
            string filter,
            string search,
            CancellationToken ct)
        {
            if (pageIndex.HasValue)
            {
                if (pageIndex <= 1) pageIndex = 1;
                PageIndex = pageIndex.Value;
            }

            var hasYearVaue = int.TryParse(search, out var year);
            var searchYear = hasYearVaue && filter == "date" ? year : 1400;
            var query = filter switch
            {
                "" => new GetLatestQuery(pageIndex ?? 1, a => a.Published),
                "date" => new GetLatestQuery(pageIndex ?? 1, a => a.Published && a.Year == searchYear),
                "tag" => new GetLatestQuery(pageIndex ?? 1, a => a.Published && a.Tags.Contains(search)),
                _ => new GetLatestQuery(pageIndex ?? 1, a => a.Published)
            };
            Filter = filter;
            Search = search;
            TimelineList = await _mediator.Send(query, ct);

            return Page();
        }
    }
}
