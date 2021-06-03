using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Irantimeline.Commands;
using Irantimeline.Helpers;
using Irantimeline.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Irantimeline.Areas.Timeline.Pages
{
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AddModel(
            IMediator mediator,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        [TempData]
        public string Message { get; set; }

        [TempData]
        public string Alert { get; set; }

        [BindProperty] public TimelineAddViewModel InputModel { get; set; }

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync(CancellationToken ct)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                Message = Consts.DefaultErrorMessage;
                Alert = "danger";
                return Page();
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            InputModel.Approved = isAdmin;
            InputModel.Published = isAdmin;
            InputModel.Resources = string.Join(",", InputModel.Resources.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.RemoveEmptyEntries));
            InputModel.RisedOn = DateTimeOffset.UtcNow;
            InputModel.CreatedByEmail = user.Email;

            var model = _mapper.Map<TimelineEntity>(InputModel);
            var command = new AddTimelineCommand(model);
            await _mediator.Send(command, ct);

            Message = Consts.DefaultSuccessMessage;
            Alert = "success";
            return RedirectToPage("Index");
        }
    }
}
