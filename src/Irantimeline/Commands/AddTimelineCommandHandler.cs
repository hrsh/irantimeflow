using Irantimeline.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Irantimeline.Commands
{
    public class AddTimelineCommandHandler : IRequestHandler<AddTimelineCommand>
    {
        private readonly ApplicationDbContext _repository;

        public AddTimelineCommandHandler(ApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
            AddTimelineCommand request,
            CancellationToken ct)
        {
            _repository.Timelines.Add(request.Model);
            await _repository.SaveChangesAsync(true, ct);
            return Unit.Value;
        }
    }
}
