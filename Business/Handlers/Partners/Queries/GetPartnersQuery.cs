
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Partners.Queries
{

    public class GetPartnersQuery : IRequest<IDataResult<IEnumerable<Partner>>>
    {
        public class GetPartnersQueryHandler : IRequestHandler<GetPartnersQuery, IDataResult<IEnumerable<Partner>>>
        {
            private readonly IPartnerRepository _partnerRepository;
            private readonly IMediator _mediator;

            public GetPartnersQueryHandler(IPartnerRepository partnerRepository, IMediator mediator)
            {
                _partnerRepository = partnerRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Partner>>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Partner>>(await _partnerRepository.GetListAsync());
            }
        }
    }
}