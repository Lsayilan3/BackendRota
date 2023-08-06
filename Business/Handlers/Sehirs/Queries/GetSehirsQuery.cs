
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Sehirs.Queries
{

    public class GetSehirsQuery : IRequest<IDataResult<IEnumerable<Sehir>>>
    {
        public class GetSehirsQueryHandler : IRequestHandler<GetSehirsQuery, IDataResult<IEnumerable<Sehir>>>
        {
            private readonly ISehirRepository _sehirRepository;
            private readonly IMediator _mediator;

            public GetSehirsQueryHandler(ISehirRepository sehirRepository, IMediator mediator)
            {
                _sehirRepository = sehirRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Sehir>>> Handle(GetSehirsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Sehir>>(await _sehirRepository.GetListAsync());
            }
        }
    }
}