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

        public TimelineListViewModel TimelineList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex, CancellationToken ct)
        {
            var query = new GetLatestQuery(pageIndex ?? 1);
            TimelineList = await _mediator.Send(query, ct);
            return Page();
        }
    }
}
