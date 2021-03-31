using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IranTimeFlow.WebApp.Commands;
using IranTimeFlow.WebApp.Helpers;
using IranTimeFlow.WebApp.Models;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IranTimeFlow.WebApp.Areas.SimpleUser.Pages
{
    public class AddTimelinePage : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AddTimelinePage(
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

            var isAdmin = await _userManager.IsInRoleAsync(user, RoleNames.Admin);
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