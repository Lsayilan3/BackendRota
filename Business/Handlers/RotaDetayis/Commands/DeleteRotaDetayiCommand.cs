
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


namespace Business.Handlers.RotaDetayis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRotaDetayiCommand : IRequest<IResult>
    {
        public int RotaDetayiId { get; set; }

        public class DeleteRotaDetayiCommandHandler : IRequestHandler<DeleteRotaDetayiCommand, IResult>
        {
            private readonly IRotaDetayiRepository _rotaDetayiRepository;
            private readonly IMediator _mediator;

            public DeleteRotaDetayiCommandHandler(IRotaDetayiRepository rotaDetayiRepository, IMediator mediator)
            {
                _rotaDetayiRepository = rotaDetayiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRotaDetayiCommand request, CancellationToken cancellationToken)
            {
                var rotaDetayiToDelete = _rotaDetayiRepository.Get(p => p.RotaDetayiId == request.RotaDetayiId);

                _rotaDetayiRepository.Delete(rotaDetayiToDelete);
                await _rotaDetayiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

