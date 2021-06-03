using Irantimeline.Models;
using MediatR;
using System.Collections.Generic;

namespace Irantimeline.Queries
{
    public class GetMyTimelineQuery : IRequest<IEnumerable<TimelineViewModel>>
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
