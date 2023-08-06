
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Gunlers.Queries
{
    public class GetGunlerQuery : IRequest<IDataResult<Gunler>>
    {
        public int GunlerId { get; set; }

        public class GetGunlerQueryHandler : IRequestHandler<GetGunlerQuery, IDataResult<Gunler>>
        {
            private readonly IGunlerRepository _gunlerRepository;
            private readonly IMediator _mediator;

            public GetGunlerQueryHandler(IGunlerRepository gunlerRepository, IMediator mediator)
            {
                _gunlerRepository = gunlerRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Gunler>> Handle(GetGunlerQuery request, CancellationToken cancellationToken)
            {
                var gunler = await _gunlerRepository.GetAsync(p => p.GunlerId == request.GunlerId);
                return new SuccessDataResult<Gunler>(gunler);
            }
        }
    }
}
