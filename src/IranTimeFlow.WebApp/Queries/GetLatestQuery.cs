using IranTimeFlow.WebApp.ViewModels;
using MediatR;

namespace IranTimeFlow.WebApp.Queries
{
    public class GetLatestQuery : IRequest<TimelineListViewModel>
    {
        public int PageIndex { get; }

        public GetLatestQuery(int pageIndex) =>
            PageIndex = pageIndex;
    }
}
