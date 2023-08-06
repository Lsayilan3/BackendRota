
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
using Business.Handlers.Puans.ValidationRules;

namespace Business.Handlers.Puans.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePuanCommand : IRequest<IResult>
    {

        public string GenelPuan { get; set; }
        public int RotaId { get; set; }
        public string Hizmetler { get; set; }
        public string HizmetlerPuan { get; set; }
        public string Konum { get; set; }
        public string KonumPuan { get; set; }
        public string Kolayliklar { get; set; }
        public string KolayliklarPuan { get; set; }
        public string Fiyat { get; set; }
        public string FiyatPuan { get; set; }
        public string Yiyecek { get; set; }
        public string YiyecekPuan { get; set; }
        public string Harita { get; set; }


        public class CreatePuanCommandHandler : IRequestHandler<CreatePuanCommand, IResult>
        {
            private readonly IPuanRepository _puanRepository;
            private readonly IMediator _mediator;
            public CreatePuanCommandHandler(IPuanRepository puanRepository, IMediator mediator)
            {
                _puanRepository = puanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePuanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePuanCommand request, CancellationToken cancellationToken)
            {
                //var isTherePuanRecord = _puanRepository.Query().Any(u => u.GenelPuan == request.GenelPuan);

                //if (isTherePuanRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPuan = new Puan
                {
                    GenelPuan = request.GenelPuan,
                    Hizmetler = request.Hizmetler,
                    HizmetlerPuan = request.HizmetlerPuan,
                    Konum = request.Konum,
                    KonumPuan = request.KonumPuan,
                    Kolayliklar = request.Kolayliklar,
                    KolayliklarPuan = request.KolayliklarPuan,
                    Fiyat = request.Fiyat,
                    FiyatPuan = request.FiyatPuan,
                    Yiyecek = request.Yiyecek,
                    YiyecekPuan = request.YiyecekPuan,
                    Harita = request.Harita,
                    RotaId = request.RotaId,

                };

                _puanRepository.Add(addedPuan);
                await _puanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}