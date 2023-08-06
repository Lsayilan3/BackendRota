
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
using Business.Handlers.Rotas.ValidationRules;


namespace Business.Handlers.Rotas.Commands
{


    public class UpdateRotaCommand : IRequest<IResult>
    {
        public int RotaId { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Aciklama { get; set; }
        public string Yayin { get; set; }
        public int Sira { get; set; }
        public string Foto { get; set; }
        public int KategoriId { get; set; }
        public int SehirId { get; set; }
        public int AnaRotaId { get; set; }

        public class UpdateRotaCommandHandler : IRequestHandler<UpdateRotaCommand, IResult>
        {
            private readonly IRotaRepository _rotaRepository;
            private readonly IMediator _mediator;

            public UpdateRotaCommandHandler(IRotaRepository rotaRepository, IMediator mediator)
            {
                _rotaRepository = rotaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRotaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRotaCommand request, CancellationToken cancellationToken)
            {
                var isThereRotaRecord = await _rotaRepository.GetAsync(u => u.RotaId == request.RotaId);


                isThereRotaRecord.Baslik = request.Baslik;
                isThereRotaRecord.Ozet = request.Ozet;
                isThereRotaRecord.Aciklama = request.Aciklama;
                isThereRotaRecord.Yayin = request.Yayin;
                isThereRotaRecord.Sira = request.Sira;
                isThereRotaRecord.Foto = request.Foto;
                isThereRotaRecord.KategoriId = request.KategoriId;
                isThereRotaRecord.SehirId = request.SehirId;
                isThereRotaRecord.AnaRotaId = request.AnaRotaId;


                _rotaRepository.Update(isThereRotaRecord);
                await _rotaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

