using IranTimeFlow.WebApp.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Commands
{
    public class EditTimelineCommandHandler : IRequestHandler<EditTimelineCommand>
    {
        private readonly IRepository _repository;

        public EditTimelineCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
            EditTimelineCommand request, 
            CancellationToken ct)
        {
            await _repository.UpdateAsync(
                request.Model, 
                ct, 
                a => a.Approved,
                a => a.Content,
                a => a.Month,
                a => a.Published,
                a => a.Resources,
                a => a.RisedOn,
                a => a.Tags,
                a => a.Title,
                a => a.Year);

            return Unit.Value;
        }
    }
}
