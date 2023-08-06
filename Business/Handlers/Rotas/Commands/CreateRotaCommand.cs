
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
using Business.Handlers.Rotas.ValidationRules;

namespace Business.Handlers.Rotas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRotaCommand : IRequest<IResult>
    {

        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Aciklama { get; set; }
        public string Yayin { get; set; }
        public int Sira { get; set; }
        public string Foto { get; set; }
        public int KategoriId { get; set; }
        public int SehirId { get; set; }
        public int AnaRotaId { get; set; }


        public class CreateRotaCommandHandler : IRequestHandler<CreateRotaCommand, IResult>
        {
            private readonly IRotaRepository _rotaRepository;
            private readonly IMediator _mediator;
            public CreateRotaCommandHandler(IRotaRepository rotaRepository, IMediator mediator)
            {
                _rotaRepository = rotaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRotaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRotaCommand request, CancellationToken cancellationToken)
            {
                //var isThereRotaRecord = _rotaRepository.Query().Any(u => u.Baslik == request.Baslik);

                //if (isThereRotaRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRota = new Rota
                {
                    Baslik = request.Baslik,
                    Ozet = request.Ozet,
                    Aciklama = request.Aciklama,
                    Yayin = request.Yayin,
                    Sira = request.Sira,
                    Foto = request.Foto,
                    KategoriId = request.KategoriId,
                    SehirId = request.SehirId,
                    AnaRotaId = request.AnaRotaId,

                };

                _rotaRepository.Add(addedRota);
                await _rotaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}