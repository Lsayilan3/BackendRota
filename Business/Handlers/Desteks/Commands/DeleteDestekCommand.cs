
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


namespace Business.Handlers.Desteks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDestekCommand : IRequest<IResult>
    {
        public int DestekId { get; set; }

        public class DeleteDestekCommandHandler : IRequestHandler<DeleteDestekCommand, IResult>
        {
            private readonly IDestekRepository _destekRepository;
            private readonly IMediator _mediator;

            public DeleteDestekCommandHandler(IDestekRepository destekRepository, IMediator mediator)
            {
                _destekRepository = destekRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDestekCommand request, CancellationToken cancellationToken)
            {
                var destekToDelete = _destekRepository.Get(p => p.DestekId == request.DestekId);

                _destekRepository.Delete(destekToDelete);
                await _destekRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

