
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Desteks.Queries
{
    public class GetDestekQuery : IRequest<IDataResult<Destek>>
    {
        public int DestekId { get; set; }

        public class GetDestekQueryHandler : IRequestHandler<GetDestekQuery, IDataResult<Destek>>
        {
            private readonly IDestekRepository _destekRepository;
            private readonly IMediator _mediator;

            public GetDestekQueryHandler(IDestekRepository destekRepository, IMediator mediator)
            {
                _destekRepository = destekRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Destek>> Handle(GetDestekQuery request, CancellationToken cancellationToken)
            {
                var destek = await _destekRepository.GetAsync(p => p.DestekId == request.DestekId);
                return new SuccessDataResult<Destek>(destek);
            }
        }
    }
}
