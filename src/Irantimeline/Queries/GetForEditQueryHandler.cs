using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irantimeline.Data;
using Irantimeline.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Irantimeline.Queries
{
    public class GetForEditQueryHandler : 
        IRequestHandler<GetForEditQuery, TimelineEditViewModel>
    {
        private readonly ApplicationDbContext _repository;
        private readonly IMapper _mapper;

        public GetForEditQueryHandler(
            ApplicationDbContext repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TimelineEditViewModel> Handle(
            GetForEditQuery request,
            CancellationToken ct)
        {
            var t = await _repository
                .Timelines
                .AsNoTracking()
                .ProjectTo<TimelineEditViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.UniqueId == request.ItemId, ct);

            return t;
        }
    }
}
