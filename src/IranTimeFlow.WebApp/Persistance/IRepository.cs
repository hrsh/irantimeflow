﻿using IranTimeFlow.WebApp.Models;
using IranTimeFlow.WebApp.ViewModels;
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

        Task UpdateAsync(TimelineEntity model, CancellationToken ct = default);

        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
