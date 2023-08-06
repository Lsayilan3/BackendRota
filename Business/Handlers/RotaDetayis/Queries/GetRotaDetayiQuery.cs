
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RotaDetayis.Queries
{
    public class GetRotaDetayiQuery : IRequest<IDataResult<RotaDetayi>>
    {
        public int RotaDetayiId { get; set; }

        public class GetRotaDetayiQueryHandler : IRequestHandler<GetRotaDetayiQuery, IDataResult<RotaDetayi>>
        {
            private readonly IRotaDetayiRepository _rotaDetayiRepository;
            private readonly IMediator _mediator;

            public GetRotaDetayiQueryHandler(IRotaDetayiRepository rotaDetayiRepository, IMediator mediator)
            {
                _rotaDetayiRepository = rotaDetayiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RotaDetayi>> Handle(GetRotaDetayiQuery request, CancellationToken cancellationToken)
            {
                var rotaDetayi = await _rotaDetayiRepository.GetAsync(p => p.RotaDetayiId == request.RotaDetayiId);
                return new SuccessDataResult<RotaDetayi>(rotaDetayi);
            }
        }
    }
}
