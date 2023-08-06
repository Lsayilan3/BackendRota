
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Sehirs.Queries
{
    public class GetSehirQuery : IRequest<IDataResult<Sehir>>
    {
        public int SehirId { get; set; }

        public class GetSehirQueryHandler : IRequestHandler<GetSehirQuery, IDataResult<Sehir>>
        {
            private readonly ISehirRepository _sehirRepository;
            private readonly IMediator _mediator;

            public GetSehirQueryHandler(ISehirRepository sehirRepository, IMediator mediator)
            {
                _sehirRepository = sehirRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Sehir>> Handle(GetSehirQuery request, CancellationToken cancellationToken)
            {
                var sehir = await _sehirRepository.GetAsync(p => p.SehirId == request.SehirId);
                return new SuccessDataResult<Sehir>(sehir);
            }
        }
    }
}
