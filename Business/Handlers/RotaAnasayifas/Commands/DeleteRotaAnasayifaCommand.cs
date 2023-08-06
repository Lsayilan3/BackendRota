
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


namespace Business.Handlers.RotaAnasayifas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRotaAnasayifaCommand : IRequest<IResult>
    {
        public int RotaAnasayifaId { get; set; }

        public class DeleteRotaAnasayifaCommandHandler : IRequestHandler<DeleteRotaAnasayifaCommand, IResult>
        {
            private readonly IRotaAnasayifaRepository _rotaAnasayifaRepository;
            private readonly IMediator _mediator;

            public DeleteRotaAnasayifaCommandHandler(IRotaAnasayifaRepository rotaAnasayifaRepository, IMediator mediator)
            {
                _rotaAnasayifaRepository = rotaAnasayifaRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRotaAnasayifaCommand request, CancellationToken cancellationToken)
            {
                var rotaAnasayifaToDelete = _rotaAnasayifaRepository.Get(p => p.RotaAnasayifaId == request.RotaAnasayifaId);

                _rotaAnasayifaRepository.Delete(rotaAnasayifaToDelete);
                await _rotaAnasayifaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

