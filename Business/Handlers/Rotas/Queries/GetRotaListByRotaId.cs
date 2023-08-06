
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

namespace Business.Handlers.Rotas.Queries
{

    public class GetRotaListByRotaId: IRequest<IDataResult<IEnumerable<Rota>>>
    {
        public int RotaId { get; set; }

        public class GetRotasQueryHandler : IRequestHandler<GetRotaListByRotaId, IDataResult<IEnumerable<Rota>>>
        {
            private readonly IRotaRepository _rotaRepository;
            private readonly IMediator _mediator;

            public GetRotasQueryHandler(IRotaRepository rotaRepository, IMediator mediator)
            {
                _rotaRepository = rotaRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Rota>>> Handle(GetRotaListByRotaId request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Rota>>(await _rotaRepository.GetListAsync(x => x.RotaId == request.RotaId));
            }
        }
    }
}
