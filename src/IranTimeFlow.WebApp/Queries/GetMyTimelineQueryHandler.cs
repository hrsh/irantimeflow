using IranTimeFlow.WebApp.PagedModel;
using IranTimeFlow.WebApp.Persistance;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Queries
{
    public class GetMyTimelineQueryHandler : IRequestHandler<GetMyTimelineQuery, PagedList<TimelineViewModel>>
    {
        private readonly IRepository _repository;

        public GetMyTimelineQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<TimelineViewModel>> Handle(
            GetMyTimelineQuery request,
            CancellationToken ct)
        {
            var t = await _repository.GetLatestPagedAsync(
                request.PageIndex,
                a => a.CreatedByEmail == request.MyEmail,
                ct);

            return t;
        }
    }
}
