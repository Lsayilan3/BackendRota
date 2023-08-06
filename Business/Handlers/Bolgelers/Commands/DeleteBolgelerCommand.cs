
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


namespace Business.Handlers.Bolgelers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBolgelerCommand : IRequest<IResult>
    {
        public int BolgelerId { get; set; }

        public class DeleteBolgelerCommandHandler : IRequestHandler<DeleteBolgelerCommand, IResult>
        {
            private readonly IBolgelerRepository _bolgelerRepository;
            private readonly IMediator _mediator;

            public DeleteBolgelerCommandHandler(IBolgelerRepository bolgelerRepository, IMediator mediator)
            {
                _bolgelerRepository = bolgelerRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBolgelerCommand request, CancellationToken cancellationToken)
            {
                var bolgelerToDelete = _bolgelerRepository.Get(p => p.BolgelerId == request.BolgelerId);

                _bolgelerRepository.Delete(bolgelerToDelete);
                await _bolgelerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

