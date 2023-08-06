
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
using Business.Handlers.RotaGaleris.ValidationRules;

namespace Business.Handlers.RotaGaleris.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRotaGaleriCommand : IRequest<IResult>
    {

        public int RotaId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int ResimTipiId { get; set; }
        


        public class CreateRotaGaleriCommandHandler : IRequestHandler<CreateRotaGaleriCommand, IResult>
        {
            private readonly IRotaGaleriRepository _rotaGaleriRepository;
            private readonly IMediator _mediator;
            public CreateRotaGaleriCommandHandler(IRotaGaleriRepository rotaGaleriRepository, IMediator mediator)
            {
                _rotaGaleriRepository = rotaGaleriRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRotaGaleriValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRotaGaleriCommand request, CancellationToken cancellationToken)
            {
                //var isThereRotaGaleriRecord = _rotaGaleriRepository.Query().Any(u => u.RotaId == request.RotaId);

                //if (isThereRotaGaleriRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRotaGaleri = new RotaGaleri
                {
                    RotaId = request.RotaId,
                    Foto = request.Foto,
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Yayin = request.Yayin,
                    ResimTipiId = request.ResimTipiId,
                  
                };

                _rotaGaleriRepository.Add(addedRotaGaleri);
                await _rotaGaleriRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}