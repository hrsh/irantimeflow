using IranTimeFlow.WebApp.PagedModel;
using IranTimeFlow.WebApp.Queries;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Pages
{
    public class IndexPage : PageModel
    {
        private readonly IMediator _mediator;

        public IndexPage(IMediator mediator)
        {
            _mediator = mediator;
        }

        public PagedList<TimelineViewModel> TimelineList { get; set; }

        public string Filter { get; set; }

        public string Search { get; set; }

        public async Task<IActionResult> OnGetAsync(
            int? pageIndex, 
            string filter,
            string search,
            CancellationToken ct)
        {
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
