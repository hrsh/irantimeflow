using IranTimeFlow.WebApp.ViewModels;
using MediatR;

namespace IranTimeFlow.WebApp.Queries
{
    public class GetForEditQuery : IRequest<TimelineEditViewModel>
    {
        public string ItemId { get; }

        public GetForEditQuery(string itemId) =>
            ItemId = itemId;
    }
}
