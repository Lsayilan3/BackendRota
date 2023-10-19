
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


namespace Business.Handlers.MediaFotoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMediaFotoCommand : IRequest<IResult>
    {
        public int MediaFotoId { get; set; }

        public class DeleteMediaFotoCommandHandler : IRequestHandler<DeleteMediaFotoCommand, IResult>
        {
            private readonly IMediaFotoRepository _mediaFotoRepository;
            private readonly IMediator _mediator;

            public DeleteMediaFotoCommandHandler(IMediaFotoRepository mediaFotoRepository, IMediator mediator)
            {
                _mediaFotoRepository = mediaFotoRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteMediaFotoCommand request, CancellationToken cancellationToken)
            {
                var mediaFotoToDelete = _mediaFotoRepository.Get(p => p.MediaFotoId == request.MediaFotoId);

                _mediaFotoRepository.Delete(mediaFotoToDelete);
                await _mediaFotoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

