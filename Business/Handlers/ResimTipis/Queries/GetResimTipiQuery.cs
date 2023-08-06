
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ResimTipis.Queries
{
    public class GetResimTipiQuery : IRequest<IDataResult<ResimTipi>>
    {
        public int ResimTipiId { get; set; }

        public class GetResimTipiQueryHandler : IRequestHandler<GetResimTipiQuery, IDataResult<ResimTipi>>
        {
            private readonly IResimTipiRepository _resimTipiRepository;
            private readonly IMediator _mediator;

            public GetResimTipiQueryHandler(IResimTipiRepository resimTipiRepository, IMediator mediator)
            {
                _resimTipiRepository = resimTipiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ResimTipi>> Handle(GetResimTipiQuery request, CancellationToken cancellationToken)
            {
                var resimTipi = await _resimTipiRepository.GetAsync(p => p.ResimTipiId == request.ResimTipiId);
                return new SuccessDataResult<ResimTipi>(resimTipi);
            }
        }
    }
}
