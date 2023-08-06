
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

namespace Business.Handlers.Puans.Queries
{

    public class GetPuansQuery : IRequest<IDataResult<IEnumerable<Puan>>>
    {
        public class GetPuansQueryHandler : IRequestHandler<GetPuansQuery, IDataResult<IEnumerable<Puan>>>
        {
            private readonly IPuanRepository _puanRepository;
            private readonly IMediator _mediator;

            public GetPuansQueryHandler(IPuanRepository puanRepository, IMediator mediator)
            {
                _puanRepository = puanRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Puan>>> Handle(GetPuansQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Puan>>(await _puanRepository.GetListAsync());
            }
        }
    }
}