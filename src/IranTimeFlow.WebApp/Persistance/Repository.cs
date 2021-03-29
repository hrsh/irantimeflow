using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreSecondLevelCacheInterceptor;
using IranTimeFlow.WebApp.DataContext;
using IranTimeFlow.WebApp.Models;
using IranTimeFlow.WebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Persistance
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 64;
        private readonly int _cacheTime = 30; // <- int minutes

        public Repository(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(TimelineEntity model, CancellationToken ct = default)
        {
            _context.Timelines.Add(model);
            await _context.SaveChangesAsync(true, ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = new TimelineEntity
            {
                Id = id
            };

            _context.Timelines.Remove(entity);
            await _context.SaveChangesAsync(true, ct);
        }

        public async Task<TimelineListViewModel> FilterByMonthAsync(int month, CancellationToken ct = default)
        {
            var t = await _context
                .Timelines
                .AsNoTracking()
                .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(_cacheTime))
                .ProjectTo<TimelineViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new() { Timelines = t };
        }

        public async Task<TimelineListViewModel> FilterByYearAsync(int year, CancellationToken ct = default)
        {
            var t = await _context
                .Timelines
                .AsNoTracking()
                .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(_cacheTime))
                .ProjectTo<TimelineViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new() { Timelines = t };
        }

        public async Task<TimelineListViewModel> GetLatestAsync(
            int pageIndex, 
            CancellationToken ct = default)
        {
            var t = await _context
                .Timelines
                .AsNoTracking()
                .Where(a => a.Published)
                .OrderByDescending(a => a.Id)
                .Skip(SkipCount())
                .Take(_pageSize)
                .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(_cacheTime))
                .ProjectTo<TimelineViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            int SkipCount() => (pageIndex - 1) * _pageSize;

            return new() { Timelines = t };
        }

        public Task UpdateAsync(TimelineEntity model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
