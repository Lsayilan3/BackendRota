
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

namespace Business.Handlers.Desteks.Queries
{

    public class GetDesteksQuery : IRequest<IDataResult<IEnumerable<Destek>>>
    {
        public class GetDesteksQueryHandler : IRequestHandler<GetDesteksQuery, IDataResult<IEnumerable<Destek>>>
        {
            private readonly IDestekRepository _destekRepository;
            private readonly IMediator _mediator;

            public GetDesteksQueryHandler(IDestekRepository destekRepository, IMediator mediator)
            {
                _destekRepository = destekRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Destek>>> Handle(GetDesteksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Destek>>(await _destekRepository.GetListAsync());
            }
        }
    }
}