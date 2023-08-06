
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


namespace Business.Handlers.RotaGaleris.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRotaGaleriCommand : IRequest<IResult>
    {
        public int RotaGaleriId { get; set; }

        public class DeleteRotaGaleriCommandHandler : IRequestHandler<DeleteRotaGaleriCommand, IResult>
        {
            private readonly IRotaGaleriRepository _rotaGaleriRepository;
            private readonly IMediator _mediator;

            public DeleteRotaGaleriCommandHandler(IRotaGaleriRepository rotaGaleriRepository, IMediator mediator)
            {
                _rotaGaleriRepository = rotaGaleriRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRotaGaleriCommand request, CancellationToken cancellationToken)
            {
                var rotaGaleriToDelete = _rotaGaleriRepository.Get(p => p.RotaGaleriId == request.RotaGaleriId);

                _rotaGaleriRepository.Delete(rotaGaleriToDelete);
                await _rotaGaleriRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

