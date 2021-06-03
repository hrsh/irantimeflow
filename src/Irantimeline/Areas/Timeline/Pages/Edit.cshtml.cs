using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Irantimeline.Commands;
using Irantimeline.Helpers;
using Irantimeline.Models;
using Irantimeline.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Irantimeline.Areas.Timeline.Pages
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditModel(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public TimelineEditViewModel InputModel { get; set; }

        [TempData] public string Message { get; set; }

        [TempData] public string Alert { get; set; }

        public async Task<IActionResult> OnGetAsync(
            string itemId, 
            CancellationToken ct)
        {
            Message = string.Empty;
            var query = new GetForEditQuery(itemId);
            InputModel = await _mediator.Send(query, ct);
            InputModel.Resources = InputModel.Resources.Replace(",", Environment.NewLine);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                Message = Consts.DefaultErrorMessage;
                Alert = "danger";
                return Page();
            }

            //var date = new DateTimeOffset(
            //    InputModel.RisedOn.Year,
            //    InputModel.RisedOn.Month,
            //    InputModel.RisedOn.Day,
            //    0,
            //    0,
            //    0,
            //    TimeSpan.Zero);

            //InputModel.RisedOn = date;
            InputModel.Resources = string.Join(",", InputModel.Resources.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries));

            var entity = _mapper.Map<TimelineEntity>(InputModel);
            var command = new EditTimelineCommand(entity);
            await _mediator.Send(command, ct);

            return RedirectToPage("./Index");
        }
    }
}
