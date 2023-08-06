
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


namespace Business.Handlers.Ulkes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteUlkeCommand : IRequest<IResult>
    {
        public int UlkeId { get; set; }

        public class DeleteUlkeCommandHandler : IRequestHandler<DeleteUlkeCommand, IResult>
        {
            private readonly IUlkeRepository _ulkeRepository;
            private readonly IMediator _mediator;

            public DeleteUlkeCommandHandler(IUlkeRepository ulkeRepository, IMediator mediator)
            {
                _ulkeRepository = ulkeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteUlkeCommand request, CancellationToken cancellationToken)
            {
                var ulkeToDelete = _ulkeRepository.Get(p => p.UlkeId == request.UlkeId);

                _ulkeRepository.Delete(ulkeToDelete);
                await _ulkeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

