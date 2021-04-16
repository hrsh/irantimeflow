using IranTimeFlow.BackService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.BackService.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimelineController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Latest(
            int? pageIndex,
            CancellationToken ct)
        {
            var query = new GetLatestQuery(pageIndex ?? 1, a => a.Published);
            var response = await _mediator.Send(query, ct);
            return Ok(response);
        }
    }
}
