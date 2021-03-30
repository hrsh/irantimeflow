using IranTimeFlow.WebApp.Persistance;
using IranTimeFlow.WebApp.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Queries
{
    public class GetForEditQueryHandler : IRequestHandler<GetForEditQuery, TimelineEditViewModel>
    {
        private readonly IRepository _repository;

        public GetForEditQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<TimelineEditViewModel> Handle(
            GetForEditQuery request, 
            CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
