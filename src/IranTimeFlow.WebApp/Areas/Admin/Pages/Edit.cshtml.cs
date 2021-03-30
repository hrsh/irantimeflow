using AutoMapper;
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

namespace IranTimeFlow.WebApp.Areas.Admin.Pages
{
    public class EditPage : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditPage(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public TimelineEditViewModel InputModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string itemId, CancellationToken ct)
        {
            //var query = new 
            return Page();
        }
    }
}
