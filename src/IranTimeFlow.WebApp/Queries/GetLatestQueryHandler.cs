using IranTimeFlow.WebApp.Persistance;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Queries
{
    public class GetLatestQueryHandler : IRequestHandler<GetLatestQuery, TimelineListViewModel>
    {
        private readonly IRepository _repository;

        public GetLatestQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<TimelineListViewModel> Handle(
            GetLatestQuery request,
            CancellationToken ct) =>
            await _repository.GetLatestAsync(request.PageIndex, ct);
    }
}
