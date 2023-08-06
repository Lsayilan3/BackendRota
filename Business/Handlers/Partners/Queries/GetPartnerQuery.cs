
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Partners.Queries
{
    public class GetPartnerQuery : IRequest<IDataResult<Partner>>
    {
        public int PartnerId { get; set; }

        public class GetPartnerQueryHandler : IRequestHandler<GetPartnerQuery, IDataResult<Partner>>
        {
            private readonly IPartnerRepository _partnerRepository;
            private readonly IMediator _mediator;

            public GetPartnerQueryHandler(IPartnerRepository partnerRepository, IMediator mediator)
            {
                _partnerRepository = partnerRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Partner>> Handle(GetPartnerQuery request, CancellationToken cancellationToken)
            {
                var partner = await _partnerRepository.GetAsync(p => p.PartnerId == request.PartnerId);
                return new SuccessDataResult<Partner>(partner);
            }
        }
    }
}
