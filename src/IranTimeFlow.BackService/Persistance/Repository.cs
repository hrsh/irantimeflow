using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreSecondLevelCacheInterceptor;
using IranTimeFlow.BackService.DataContext;
using IranTimeFlow.BackService.Models;
using IranTimeFlow.BackService.PagedModel;
using IranTimeFlow.BackService.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.BackService.Persistance
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 32;
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

        public async Task<TimelineEditViewModel> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var t = await _context
                .Timelines
                .AsNoTracking()
                .ProjectTo<TimelineEditViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.UniqueId == id, ct);

            return t;
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

        public async Task<PagedList<TimelineViewModel>> GetLatestPagedAsync(
            int pageIndex,
            Expression<Func<TimelineEntity, bool>> predicate,
            CancellationToken ct = default)
        {
            var query = _context
                            .Timelines
                            .AsNoTracking()
                            .Where(predicate)
                            .OrderByDescending(a => a.Id)
                            .ProjectTo<TimelineViewModel>(_mapper.ConfigurationProvider)
                            .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(_cacheTime));

            var result = await query.AsPagedList(pageIndex, _pageSize, ct);

            return result;
        }

        public async Task UpdateAsync(
            TimelineEntity model,
            CancellationToken ct = default,
            params Expression<Func<TimelineEntity, object>>[] updatedProperties)
        {
            var entity = await _context.Timelines.FindAsync(new object[] { model.Id }, ct);

            var m = _context.Entry(model);
            var e = _context.Entry(entity);

            if (updatedProperties.Any())
                foreach (var property in updatedProperties)
                {
                    var currentPropertyName = GetMemberName(property.Body);

                    e.Property(currentPropertyName).CurrentValue =
                        m.Property(currentPropertyName).CurrentValue;

                    _context.Entry(entity).Property(currentPropertyName).IsModified = true;
                }

            await _context.SaveChangesAsync(true, ct);
        }

        private static string GetMemberName(Expression expression)
        {
            return expression.NodeType switch
            {
                ExpressionType.MemberAccess => ((MemberExpression)expression).Member.Name,
                ExpressionType.Convert => GetMemberName(((UnaryExpression)expression).Operand),
                _ => throw new NotSupportedException(expression.NodeType.ToString()),
            };
        }
    }
}
