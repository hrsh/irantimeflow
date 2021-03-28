using IranTimeFlow.WebApp.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Commands
{
    public class AddTimelineCommandHandler : IRequestHandler<AddTimelineCommand>
    {
        private readonly IRepository _repository;

        public AddTimelineCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
            AddTimelineCommand request,
            CancellationToken ct)
        {
            await _repository.CreateAsync(request.Model, ct);
            return Unit.Value;
        }
    }
}
