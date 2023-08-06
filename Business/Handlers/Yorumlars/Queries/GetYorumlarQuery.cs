
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Yorumlars.Queries
{
    public class GetYorumlarQuery : IRequest<IDataResult<Yorumlar>>
    {
        public int YorumlarId { get; set; }

        public class GetYorumlarQueryHandler : IRequestHandler<GetYorumlarQuery, IDataResult<Yorumlar>>
        {
            private readonly IYorumlarRepository _yorumlarRepository;
            private readonly IMediator _mediator;

            public GetYorumlarQueryHandler(IYorumlarRepository yorumlarRepository, IMediator mediator)
            {
                _yorumlarRepository = yorumlarRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Yorumlar>> Handle(GetYorumlarQuery request, CancellationToken cancellationToken)
            {
                var yorumlar = await _yorumlarRepository.GetAsync(p => p.YorumlarId == request.YorumlarId);
                return new SuccessDataResult<Yorumlar>(yorumlar);
            }
        }
    }
}
