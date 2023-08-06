
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


namespace Business.Handlers.Sehirs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSehirCommand : IRequest<IResult>
    {
        public int SehirId { get; set; }

        public class DeleteSehirCommandHandler : IRequestHandler<DeleteSehirCommand, IResult>
        {
            private readonly ISehirRepository _sehirRepository;
            private readonly IMediator _mediator;

            public DeleteSehirCommandHandler(ISehirRepository sehirRepository, IMediator mediator)
            {
                _sehirRepository = sehirRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSehirCommand request, CancellationToken cancellationToken)
            {
                var sehirToDelete = _sehirRepository.Get(p => p.SehirId == request.SehirId);

                _sehirRepository.Delete(sehirToDelete);
                await _sehirRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

