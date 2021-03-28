using IranTimeFlow.WebApp.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Persistance
{
    public interface IRepository
    {
        Task<IEnumerable<TimelineEntity>> GetLatestAsync(CancellationToken ct = default);

        Task<IEnumerable<TimelineEntity>> FilterByYearAsync(int year, CancellationToken ct = default);

        Task<IEnumerable<TimelineEntity>> FilterByMonthAsync(int month, CancellationToken ct = default);

        Task CreateAsync(TimelineEntity model, CancellationToken ct = default);

        Task UpdateAsync(TimelineEntity model, CancellationToken ct = default);

        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
