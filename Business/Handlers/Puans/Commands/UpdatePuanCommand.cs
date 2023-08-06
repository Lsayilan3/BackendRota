
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
using Business.Handlers.Puans.ValidationRules;


namespace Business.Handlers.Puans.Commands
{


    public class UpdatePuanCommand : IRequest<IResult>
    {
        public int PuanId { get; set; }
        public int RotaId { get; set; }
        public string GenelPuan { get; set; }
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

        public class UpdatePuanCommandHandler : IRequestHandler<UpdatePuanCommand, IResult>
        {
            private readonly IPuanRepository _puanRepository;
            private readonly IMediator _mediator;

            public UpdatePuanCommandHandler(IPuanRepository puanRepository, IMediator mediator)
            {
                _puanRepository = puanRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePuanValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePuanCommand request, CancellationToken cancellationToken)
            {
                var isTherePuanRecord = await _puanRepository.GetAsync(u => u.PuanId == request.PuanId);


                isTherePuanRecord.GenelPuan = request.GenelPuan;
                isTherePuanRecord.Hizmetler = request.Hizmetler;
                isTherePuanRecord.HizmetlerPuan = request.HizmetlerPuan;
                isTherePuanRecord.Konum = request.Konum;
                isTherePuanRecord.KonumPuan = request.KonumPuan;
                isTherePuanRecord.Kolayliklar = request.Kolayliklar;
                isTherePuanRecord.KolayliklarPuan = request.KolayliklarPuan;
                isTherePuanRecord.Fiyat = request.Fiyat;
                isTherePuanRecord.FiyatPuan = request.FiyatPuan;
                isTherePuanRecord.Yiyecek = request.Yiyecek;
                isTherePuanRecord.YiyecekPuan = request.YiyecekPuan;
                isTherePuanRecord.Harita = request.Harita;
                isTherePuanRecord.RotaId = request.RotaId;


                _puanRepository.Update(isTherePuanRecord);
                await _puanRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

