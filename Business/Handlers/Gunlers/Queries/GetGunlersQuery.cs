
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

namespace Business.Handlers.Gunlers.Queries
{

    public class GetGunlersQuery : IRequest<IDataResult<IEnumerable<Gunler>>>
    {
        public class GetGunlersQueryHandler : IRequestHandler<GetGunlersQuery, IDataResult<IEnumerable<Gunler>>>
        {
            private readonly IGunlerRepository _gunlerRepository;
            private readonly IMediator _mediator;

            public GetGunlersQueryHandler(IGunlerRepository gunlerRepository, IMediator mediator)
            {
                _gunlerRepository = gunlerRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Gunler>>> Handle(GetGunlersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Gunler>>(await _gunlerRepository.GetListAsync());
            }
        }
    }
}