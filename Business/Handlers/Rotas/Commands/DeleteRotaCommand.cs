
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Rotas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRotaCommand : IRequest<IResult>
    {
        public int RotaId { get; set; }

        public class DeleteRotaCommandHandler : IRequestHandler<DeleteRotaCommand, IResult>
        {
            private readonly IRotaRepository _rotaRepository;
            private readonly IMediator _mediator;

            public DeleteRotaCommandHandler(IRotaRepository rotaRepository, IMediator mediator)
            {
                _rotaRepository = rotaRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRotaCommand request, CancellationToken cancellationToken)
            {
                var rotaToDelete = _rotaRepository.Get(p => p.RotaId == request.RotaId);

                _rotaRepository.Delete(rotaToDelete);
                await _rotaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

