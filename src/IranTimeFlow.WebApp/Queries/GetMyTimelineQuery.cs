using IranTimeFlow.WebApp.PagedModel;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;

namespace IranTimeFlow.WebApp.Queries
{
    public class GetMyTimelineQuery : IRequest<PagedList<TimelineViewModel>>
    {
        public string MyEmail { get; }

        public int PageIndex { get; set; }

        public GetMyTimelineQuery(string myEmail, int pageIndex)
        {
            MyEmail = myEmail;
            PageIndex = pageIndex;
        }
    }
}
