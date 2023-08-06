
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Puans.Queries
{
    public class GetPuanQuery : IRequest<IDataResult<Puan>>
    {
        public int PuanId { get; set; }

        public class GetPuanQueryHandler : IRequestHandler<GetPuanQuery, IDataResult<Puan>>
        {
            private readonly IPuanRepository _puanRepository;
            private readonly IMediator _mediator;

            public GetPuanQueryHandler(IPuanRepository puanRepository, IMediator mediator)
            {
                _puanRepository = puanRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Puan>> Handle(GetPuanQuery request, CancellationToken cancellationToken)
            {
                var puan = await _puanRepository.GetAsync(p => p.PuanId == request.PuanId);
                return new SuccessDataResult<Puan>(puan);
            }
        }
    }
}
