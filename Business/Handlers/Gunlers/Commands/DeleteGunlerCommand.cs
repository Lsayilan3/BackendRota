
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


namespace Business.Handlers.Gunlers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteGunlerCommand : IRequest<IResult>
    {
        public int GunlerId { get; set; }

        public class DeleteGunlerCommandHandler : IRequestHandler<DeleteGunlerCommand, IResult>
        {
            private readonly IGunlerRepository _gunlerRepository;
            private readonly IMediator _mediator;

            public DeleteGunlerCommandHandler(IGunlerRepository gunlerRepository, IMediator mediator)
            {
                _gunlerRepository = gunlerRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGunlerCommand request, CancellationToken cancellationToken)
            {
                var gunlerToDelete = _gunlerRepository.Get(p => p.GunlerId == request.GunlerId);

                _gunlerRepository.Delete(gunlerToDelete);
                await _gunlerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

