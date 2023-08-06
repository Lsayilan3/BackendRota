
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

namespace Business.Handlers.RotaAnasayifas.Queries
{

    public class GetRotaAnasayifaListByRotaId : IRequest<IDataResult<IEnumerable<RotaAnasayifa>>>
    {
        public int RotaId { get; set; }

        public class GetRotaAnasayifasQueryHandler : IRequestHandler<GetRotaAnasayifaListByRotaId, IDataResult<IEnumerable<RotaAnasayifa>>>
        {
            private readonly IRotaAnasayifaRepository _rotaAnasayifaRepository;
            private readonly IMediator _mediator;

            public GetRotaAnasayifasQueryHandler(IRotaAnasayifaRepository rotaAnasayifaRepository, IMediator mediator)
            {
                _rotaAnasayifaRepository = rotaAnasayifaRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RotaAnasayifa>>> Handle(GetRotaAnasayifaListByRotaId request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RotaAnasayifa>>(await _rotaAnasayifaRepository.GetListAsync(x => x.RotaId == request.RotaId));
            }
        }
    }
}
