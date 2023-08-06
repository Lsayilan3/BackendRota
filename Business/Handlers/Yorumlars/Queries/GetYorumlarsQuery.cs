
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

namespace Business.Handlers.Yorumlars.Queries
{

    public class GetYorumlarsQuery : IRequest<IDataResult<IEnumerable<Yorumlar>>>
    {
        public class GetYorumlarsQueryHandler : IRequestHandler<GetYorumlarsQuery, IDataResult<IEnumerable<Yorumlar>>>
        {
            private readonly IYorumlarRepository _yorumlarRepository;
            private readonly IMediator _mediator;

            public GetYorumlarsQueryHandler(IYorumlarRepository yorumlarRepository, IMediator mediator)
            {
                _yorumlarRepository = yorumlarRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Yorumlar>>> Handle(GetYorumlarsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Yorumlar>>(await _yorumlarRepository.GetListAsync());
            }
        }
    }
}