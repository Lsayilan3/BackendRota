
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

namespace Business.Handlers.ResimTipis.Queries
{

    public class GetResimTipisQuery : IRequest<IDataResult<IEnumerable<ResimTipi>>>
    {
        public class GetResimTipisQueryHandler : IRequestHandler<GetResimTipisQuery, IDataResult<IEnumerable<ResimTipi>>>
        {
            private readonly IResimTipiRepository _resimTipiRepository;
            private readonly IMediator _mediator;

            public GetResimTipisQueryHandler(IResimTipiRepository resimTipiRepository, IMediator mediator)
            {
                _resimTipiRepository = resimTipiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ResimTipi>>> Handle(GetResimTipisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ResimTipi>>(await _resimTipiRepository.GetListAsync());
            }
        }
    }
}