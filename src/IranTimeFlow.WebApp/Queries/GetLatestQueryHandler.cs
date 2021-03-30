using IranTimeFlow.WebApp.PagedModel;
using IranTimeFlow.WebApp.Persistance;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Queries
{
    public class GetLatestQueryHandler : IRequestHandler<GetLatestQuery, PagedList<TimelineViewModel>>
    {
        private readonly IRepository _repository;

        public GetLatestQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<TimelineViewModel>> Handle(
            GetLatestQuery request,
            CancellationToken ct) =>
            //await _repository.GetLatestAsync(request.PageIndex, ct);
            await _repository.GetLatestPagedAsync(request.PageIndex, request.Predicate, ct);
    }
}
