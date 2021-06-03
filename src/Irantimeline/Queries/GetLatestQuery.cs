using Irantimeline.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Irantimeline.Queries
{
    public class GetLatestQuery : IRequest<IEnumerable<TimelineViewModel>>
    {
        public int PageIndex { get; }

        public Expression<Func<TimelineEntity, bool>> Predicate { get; }

        public GetLatestQuery(
            int pageIndex, Expression<Func<TimelineEntity, bool>> predicate) =>
            (PageIndex, Predicate) = (pageIndex, predicate);
    }
}
