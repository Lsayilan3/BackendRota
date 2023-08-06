
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


namespace Business.Handlers.Puans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePuanCommand : IRequest<IResult>
    {
        public int PuanId { get; set; }

        public class DeletePuanCommandHandler : IRequestHandler<DeletePuanCommand, IResult>
        {
            private readonly IPuanRepository _puanRepository;
            private readonly IMediator _mediator;

            public DeletePuanCommandHandler(IPuanRepository puanRepository, IMediator mediator)
            {
                _puanRepository = puanRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePuanCommand request, CancellationToken cancellationToken)
            {
                var puanToDelete = _puanRepository.Get(p => p.PuanId == request.PuanId);

                _puanRepository.Delete(puanToDelete);
                await _puanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

