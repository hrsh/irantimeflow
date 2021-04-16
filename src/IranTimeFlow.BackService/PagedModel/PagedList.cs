using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.BackService.PagedModel
{
    [Serializable]
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public PagedList(List<T> source, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(source);
        }
    }

    public static class PagedListExtensions
    {
        public static async Task<PagedList<T>> AsPagedList<T>(
            this IQueryable<T> source,
            int pageIndex,
            int pageSize,
            CancellationToken ct = default)
        {
            var count = await source.CountAsync(cancellationToken: ct);
            var items = await source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
