using IranTimeFlow.WebApp.DataContext;
using IranTimeFlow.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Persistance
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<TimelineEntity>> FilterByMonthAsync(int month, CancellationToken ct = default)
        {
            var t = await _context
                .Timelines
                .AsNoTracking()
                .Where(a => a.Month == month)
                .ToListAsync(ct);

            return t;
        }

        public async Task<IEnumerable<TimelineEntity>> FilterByYearAsync(int year, CancellationToken ct = default)
        {
            var t = await _context
                .Timelines
                .AsNoTracking()
                .Where(a => a.Year == year)
                .ToListAsync(ct);

            return t;
        }

        public async Task<IEnumerable<TimelineEntity>> GetLatestAsync(CancellationToken ct = default)
        {
            var t = await _context
                .Timelines
                .AsNoTracking()
                .ToListAsync();

            return t;
        }

        public Task UpdateAsync(TimelineEntity model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
