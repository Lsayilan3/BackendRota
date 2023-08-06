
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


namespace Business.Handlers.Partners.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePartnerCommand : IRequest<IResult>
    {
        public int PartnerId { get; set; }

        public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand, IResult>
        {
            private readonly IPartnerRepository _partnerRepository;
            private readonly IMediator _mediator;

            public DeletePartnerCommandHandler(IPartnerRepository partnerRepository, IMediator mediator)
            {
                _partnerRepository = partnerRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
            {
                var partnerToDelete = _partnerRepository.Get(p => p.PartnerId == request.PartnerId);

                _partnerRepository.Delete(partnerToDelete);
                await _partnerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

