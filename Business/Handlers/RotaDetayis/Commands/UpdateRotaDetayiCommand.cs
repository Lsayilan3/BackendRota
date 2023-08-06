
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
using Business.Handlers.RotaDetayis.ValidationRules;


namespace Business.Handlers.RotaDetayis.Commands
{


    public class UpdateRotaDetayiCommand : IRequest<IResult>
    {
        public int RotaDetayiId { get; set; }
        public int RotaId { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Aciklama { get; set; }
        public string Yayin { get; set; }
        public int Sira { get; set; }
        public string Foto { get; set; }
        public int KategoriId { get; set; }
        public int SehirId { get; set; }

        public class UpdateRotaDetayiCommandHandler : IRequestHandler<UpdateRotaDetayiCommand, IResult>
        {
            private readonly IRotaDetayiRepository _rotaDetayiRepository;
            private readonly IMediator _mediator;

            public UpdateRotaDetayiCommandHandler(IRotaDetayiRepository rotaDetayiRepository, IMediator mediator)
            {
                _rotaDetayiRepository = rotaDetayiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRotaDetayiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRotaDetayiCommand request, CancellationToken cancellationToken)
            {
                var isThereRotaDetayiRecord = await _rotaDetayiRepository.GetAsync(u => u.RotaDetayiId == request.RotaDetayiId);


                isThereRotaDetayiRecord.RotaId = request.RotaId;
                isThereRotaDetayiRecord.Baslik = request.Baslik;
                isThereRotaDetayiRecord.Ozet = request.Ozet;
                isThereRotaDetayiRecord.Aciklama = request.Aciklama;
                isThereRotaDetayiRecord.Yayin = request.Yayin;
                isThereRotaDetayiRecord.Sira = request.Sira;
                isThereRotaDetayiRecord.Foto = request.Foto;
                isThereRotaDetayiRecord.KategoriId = request.KategoriId;
                isThereRotaDetayiRecord.SehirId = request.SehirId;


                _rotaDetayiRepository.Update(isThereRotaDetayiRecord);
                await _rotaDetayiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

