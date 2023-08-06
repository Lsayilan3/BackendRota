
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

namespace Business.Handlers.Ulkes.Queries
{

    public class GetUlkesQuery : IRequest<IDataResult<IEnumerable<Ulke>>>
    {
        public class GetUlkesQueryHandler : IRequestHandler<GetUlkesQuery, IDataResult<IEnumerable<Ulke>>>
        {
            private readonly IUlkeRepository _ulkeRepository;
            private readonly IMediator _mediator;

            public GetUlkesQueryHandler(IUlkeRepository ulkeRepository, IMediator mediator)
            {
                _ulkeRepository = ulkeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Ulke>>> Handle(GetUlkesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Ulke>>(await _ulkeRepository.GetListAsync());
            }
        }
    }
}