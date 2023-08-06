
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RotaGaleris.Queries
{
    public class GetRotaGaleriQuery : IRequest<IDataResult<RotaGaleri>>
    {
        public int RotaGaleriId { get; set; }

        public class GetRotaGaleriQueryHandler : IRequestHandler<GetRotaGaleriQuery, IDataResult<RotaGaleri>>
        {
            private readonly IRotaGaleriRepository _rotaGaleriRepository;
            private readonly IMediator _mediator;

            public GetRotaGaleriQueryHandler(IRotaGaleriRepository rotaGaleriRepository, IMediator mediator)
            {
                _rotaGaleriRepository = rotaGaleriRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RotaGaleri>> Handle(GetRotaGaleriQuery request, CancellationToken cancellationToken)
            {
                var rotaGaleri = await _rotaGaleriRepository.GetAsync(p => p.RotaGaleriId == request.RotaGaleriId);
                return new SuccessDataResult<RotaGaleri>(rotaGaleri);
            }
        }
    }
}
