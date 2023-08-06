
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RotaAnasayifas.Queries
{
    public class GetRotaAnasayifaQuery : IRequest<IDataResult<RotaAnasayifa>>
    {
        public int RotaAnasayifaId { get; set; }

        public class GetRotaAnasayifaQueryHandler : IRequestHandler<GetRotaAnasayifaQuery, IDataResult<RotaAnasayifa>>
        {
            private readonly IRotaAnasayifaRepository _rotaAnasayifaRepository;
            private readonly IMediator _mediator;

            public GetRotaAnasayifaQueryHandler(IRotaAnasayifaRepository rotaAnasayifaRepository, IMediator mediator)
            {
                _rotaAnasayifaRepository = rotaAnasayifaRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RotaAnasayifa>> Handle(GetRotaAnasayifaQuery request, CancellationToken cancellationToken)
            {
                var rotaAnasayifa = await _rotaAnasayifaRepository.GetAsync(p => p.RotaAnasayifaId == request.RotaAnasayifaId);
                return new SuccessDataResult<RotaAnasayifa>(rotaAnasayifa);
            }
        }
    }
}
