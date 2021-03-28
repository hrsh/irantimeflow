using AutoMapper;
using IranTimeFlow.WebApp.Commands;
using IranTimeFlow.WebApp.Models;
using IranTimeFlow.WebApp.Queries;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TimelineController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> OnGetAsync(
            [FromBody] TimelineAddViewModel model,
            CancellationToken ct)
        {
            var entity = _mapper.Map<TimelineEntity>(model);
            var command = new AddTimelineCommand(entity);
            await _mediator.Send(command, ct);
            return NoContent();
        }

        [HttpGet("latest")]
        public async Task<IActionResult> Latest(
            int? pageIndex,
            CancellationToken ct)
        {
            var query = new GetLatestQuery(pageIndex ?? 1);
            var response = await _mediator.Send(query, ct);
            return Ok(response);
        }
    }
}
