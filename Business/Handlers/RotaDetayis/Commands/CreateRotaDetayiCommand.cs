
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.RotaDetayis.ValidationRules;

namespace Business.Handlers.RotaDetayis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRotaDetayiCommand : IRequest<IResult>
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


        public class CreateRotaDetayiCommandHandler : IRequestHandler<CreateRotaDetayiCommand, IResult>
        {
            private readonly IRotaDetayiRepository _rotaDetayiRepository;
            private readonly IMediator _mediator;
            public CreateRotaDetayiCommandHandler(IRotaDetayiRepository rotaDetayiRepository, IMediator mediator)
            {
                _rotaDetayiRepository = rotaDetayiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRotaDetayiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRotaDetayiCommand request, CancellationToken cancellationToken)
            {
                //var isThereRotaDetayiRecord = _rotaDetayiRepository.Query().Any(u => u.RotaId == request.RotaId);

                //if (isThereRotaDetayiRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRotaDetayi = new RotaDetayi
                {
                    RotaId = request.RotaId,
                    Baslik = request.Baslik,
                    Ozet = request.Ozet,
                    Aciklama = request.Aciklama,
                    Yayin = request.Yayin,
                    Sira = request.Sira,
                    Foto = request.Foto,
                    KategoriId = request.KategoriId,
                    SehirId = request.SehirId,

                };

                _rotaDetayiRepository.Add(addedRotaDetayi);
                await _rotaDetayiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}