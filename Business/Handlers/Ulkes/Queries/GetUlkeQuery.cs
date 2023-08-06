
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Ulkes.Queries
{
    public class GetUlkeQuery : IRequest<IDataResult<Ulke>>
    {
        public int UlkeId { get; set; }

        public class GetUlkeQueryHandler : IRequestHandler<GetUlkeQuery, IDataResult<Ulke>>
        {
            private readonly IUlkeRepository _ulkeRepository;
            private readonly IMediator _mediator;

            public GetUlkeQueryHandler(IUlkeRepository ulkeRepository, IMediator mediator)
            {
                _ulkeRepository = ulkeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Ulke>> Handle(GetUlkeQuery request, CancellationToken cancellationToken)
            {
                var ulke = await _ulkeRepository.GetAsync(p => p.UlkeId == request.UlkeId);
                return new SuccessDataResult<Ulke>(ulke);
            }
        }
    }
}
