
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


namespace Business.Handlers.ResimTipis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteResimTipiCommand : IRequest<IResult>
    {
        public int ResimTipiId { get; set; }

        public class DeleteResimTipiCommandHandler : IRequestHandler<DeleteResimTipiCommand, IResult>
        {
            private readonly IResimTipiRepository _resimTipiRepository;
            private readonly IMediator _mediator;

            public DeleteResimTipiCommandHandler(IResimTipiRepository resimTipiRepository, IMediator mediator)
            {
                _resimTipiRepository = resimTipiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteResimTipiCommand request, CancellationToken cancellationToken)
            {
                var resimTipiToDelete = _resimTipiRepository.Get(p => p.ResimTipiId == request.ResimTipiId);

                _resimTipiRepository.Delete(resimTipiToDelete);
                await _resimTipiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

