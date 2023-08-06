
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

namespace Business.Handlers.Bolgelers.Queries
{

    public class GetBolgelersQuery : IRequest<IDataResult<IEnumerable<Bolgeler>>>
    {
        public class GetBolgelersQueryHandler : IRequestHandler<GetBolgelersQuery, IDataResult<IEnumerable<Bolgeler>>>
        {
            private readonly IBolgelerRepository _bolgelerRepository;
            private readonly IMediator _mediator;

            public GetBolgelersQueryHandler(IBolgelerRepository bolgelerRepository, IMediator mediator)
            {
                _bolgelerRepository = bolgelerRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Bolgeler>>> Handle(GetBolgelersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Bolgeler>>(await _bolgelerRepository.GetListAsync());
            }
        }
    }
}