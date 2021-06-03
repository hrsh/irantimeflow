using Irantimeline.Models;
using MediatR;

namespace Irantimeline.Queries
{
    public class GetForEditQuery : IRequest<TimelineEditViewModel>
    {
        public string ItemId { get; }

        public GetForEditQuery(string itemId) =>
            ItemId = itemId;
    }
}
