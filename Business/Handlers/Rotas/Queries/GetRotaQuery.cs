
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Rotas.Queries
{
    public class GetRotaQuery : IRequest<IDataResult<Rota>>
    {
        public int RotaId { get; set; }

        public class GetRotaQueryHandler : IRequestHandler<GetRotaQuery, IDataResult<Rota>>
        {
            private readonly IRotaRepository _rotaRepository;
            private readonly IMediator _mediator;

            public GetRotaQueryHandler(IRotaRepository rotaRepository, IMediator mediator)
            {
                _rotaRepository = rotaRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Rota>> Handle(GetRotaQuery request, CancellationToken cancellationToken)
            {
                var rota = await _rotaRepository.GetAsync(p => p.RotaId == request.RotaId);
                return new SuccessDataResult<Rota>(rota);
            }
        }
    }
}
