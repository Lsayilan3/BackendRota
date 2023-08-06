
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Bolgelers.Queries
{
    public class GetBolgelerQuery : IRequest<IDataResult<Bolgeler>>
    {
        public int BolgelerId { get; set; }

        public class GetBolgelerQueryHandler : IRequestHandler<GetBolgelerQuery, IDataResult<Bolgeler>>
        {
            private readonly IBolgelerRepository _bolgelerRepository;
            private readonly IMediator _mediator;

            public GetBolgelerQueryHandler(IBolgelerRepository bolgelerRepository, IMediator mediator)
            {
                _bolgelerRepository = bolgelerRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Bolgeler>> Handle(GetBolgelerQuery request, CancellationToken cancellationToken)
            {
                var bolgeler = await _bolgelerRepository.GetAsync(p => p.BolgelerId == request.BolgelerId);
                return new SuccessDataResult<Bolgeler>(bolgeler);
            }
        }
    }
}
