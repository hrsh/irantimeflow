using IranTimeFlow.WebApp.Models;
using IranTimeFlow.WebApp.PagedModel;
using IranTimeFlow.WebApp.ViewModels;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Persistance
{
    public interface IRepository
    {
        Task<TimelineListViewModel> GetLatestAsync(int pageIndex, CancellationToken ct = default);

        Task<TimelineListViewModel> FilterByYearAsync(int year, CancellationToken ct = default);

        Task<TimelineListViewModel> FilterByMonthAsync(int month, CancellationToken ct = default);

        Task CreateAsync(TimelineEntity model, CancellationToken ct = default);

        Task UpdateAsync(
            TimelineEntity model, 
            CancellationToken ct = default,
            params Expression<Func<TimelineEntity, object>>[] updatedProperties);

        Task DeleteAsync(int id, CancellationToken ct = default);

        Task<PagedList<TimelineViewModel>> GetLatestPagedAsync(
            int pageIndex,
            Expression<Func<TimelineEntity, bool>> predicate,
            CancellationToken ct = default);

        Task<TimelineEditViewModel> GetByIdAsync(string id, CancellationToken ct = default);
    }
}
