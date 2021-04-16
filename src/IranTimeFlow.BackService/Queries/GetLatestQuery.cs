using IranTimeFlow.BackService.Models;
using IranTimeFlow.BackService.PagedModel;
using IranTimeFlow.BackService.ViewModels;
using MediatR;
using System;
using System.Linq.Expressions;

namespace IranTimeFlow.BackService.Queries
{
    public class GetLatestQuery : IRequest<PagedList<TimelineViewModel>>
    {
        public int PageIndex { get; }

        public Expression<Func<TimelineEntity, bool>> Predicate { get; }

        public GetLatestQuery(
            int pageIndex, Expression<Func<TimelineEntity, bool>> predicate) =>
            (PageIndex, Predicate) = (pageIndex, predicate);
    }
}
