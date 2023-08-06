
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.RotaAnasayifas.ValidationRules;


namespace Business.Handlers.RotaAnasayifas.Commands
{


    public class UpdateRotaAnasayifaCommand : IRequest<IResult>
    {
        public int RotaAnasayifaId { get; set; }
        public int RotaId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Col { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }

        public class UpdateRotaAnasayifaCommandHandler : IRequestHandler<UpdateRotaAnasayifaCommand, IResult>
        {
            private readonly IRotaAnasayifaRepository _rotaAnasayifaRepository;
            private readonly IMediator _mediator;

            public UpdateRotaAnasayifaCommandHandler(IRotaAnasayifaRepository rotaAnasayifaRepository, IMediator mediator)
            {
                _rotaAnasayifaRepository = rotaAnasayifaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRotaAnasayifaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRotaAnasayifaCommand request, CancellationToken cancellationToken)
            {
                var isThereRotaAnasayifaRecord = await _rotaAnasayifaRepository.GetAsync(u => u.RotaAnasayifaId == request.RotaAnasayifaId);


                isThereRotaAnasayifaRecord.RotaId = request.RotaId;
                isThereRotaAnasayifaRecord.Foto = request.Foto;
                isThereRotaAnasayifaRecord.Baslik = request.Baslik;
                isThereRotaAnasayifaRecord.Aciklama = request.Aciklama;
                isThereRotaAnasayifaRecord.Col = request.Col;
                isThereRotaAnasayifaRecord.Yayin = request.Yayin;
                isThereRotaAnasayifaRecord.Sira = request.Sira;


                _rotaAnasayifaRepository.Update(isThereRotaAnasayifaRecord);
                await _rotaAnasayifaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

