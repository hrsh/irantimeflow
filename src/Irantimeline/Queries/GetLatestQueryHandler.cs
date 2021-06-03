using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreSecondLevelCacheInterceptor;
using Irantimeline.Data;
using Irantimeline.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Irantimeline.Queries
{
    public class GetLatestQueryHandler : 
        IRequestHandler<GetLatestQuery, IEnumerable<TimelineViewModel>>
    {
        private readonly ApplicationDbContext _repository;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 12;
        private readonly int _cacheTime = 30; // <- int minutes

        public GetLatestQueryHandler(
            ApplicationDbContext repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimelineViewModel>> Handle(
            GetLatestQuery request,
            CancellationToken ct)
        {
            var query = _repository
                .Timelines
                .AsNoTracking()
                .Where(request.Predicate)
                .OrderByDescending(a => a.Id)
                .Skip(SkipCount())
                .Take(_pageSize)
                .ProjectTo<TimelineViewModel>(_mapper.ConfigurationProvider)
                .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(_cacheTime));

            int SkipCount() => ((request.PageIndex < 1 ? 1 : request.PageIndex) - 1) * _pageSize;

            return await query.ToListAsync(ct);
        }
    }
}
