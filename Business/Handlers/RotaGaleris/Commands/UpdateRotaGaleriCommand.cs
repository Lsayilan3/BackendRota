
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
using Business.Handlers.RotaGaleris.ValidationRules;


namespace Business.Handlers.RotaGaleris.Commands
{


    public class UpdateRotaGaleriCommand : IRequest<IResult>
    {
        public int RotaGaleriId { get; set; }
        public int RotaId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int ResimTipiId { get; set; }

        public class UpdateRotaGaleriCommandHandler : IRequestHandler<UpdateRotaGaleriCommand, IResult>
        {
            private readonly IRotaGaleriRepository _rotaGaleriRepository;
            private readonly IMediator _mediator;

            public UpdateRotaGaleriCommandHandler(IRotaGaleriRepository rotaGaleriRepository, IMediator mediator)
            {
                _rotaGaleriRepository = rotaGaleriRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRotaGaleriValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRotaGaleriCommand request, CancellationToken cancellationToken)
            {
                var isThereRotaGaleriRecord = await _rotaGaleriRepository.GetAsync(u => u.RotaGaleriId == request.RotaGaleriId);


                isThereRotaGaleriRecord.RotaId = request.RotaId;
                isThereRotaGaleriRecord.Foto = request.Foto;
                isThereRotaGaleriRecord.Baslik = request.Baslik;
                isThereRotaGaleriRecord.Aciklama = request.Aciklama;
                isThereRotaGaleriRecord.Yayin = request.Yayin;
                isThereRotaGaleriRecord.ResimTipiId = request.ResimTipiId;


                _rotaGaleriRepository.Update(isThereRotaGaleriRecord);
                await _rotaGaleriRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

