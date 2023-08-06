
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
using Business.Handlers.RotaAnasayifas.ValidationRules;

namespace Business.Handlers.RotaAnasayifas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRotaAnasayifaCommand : IRequest<IResult>
    {

        public int RotaId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Col { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }


        public class CreateRotaAnasayifaCommandHandler : IRequestHandler<CreateRotaAnasayifaCommand, IResult>
        {
            private readonly IRotaAnasayifaRepository _rotaAnasayifaRepository;
            private readonly IMediator _mediator;
            public CreateRotaAnasayifaCommandHandler(IRotaAnasayifaRepository rotaAnasayifaRepository, IMediator mediator)
            {
                _rotaAnasayifaRepository = rotaAnasayifaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRotaAnasayifaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRotaAnasayifaCommand request, CancellationToken cancellationToken)
            {
                //var isThereRotaAnasayifaRecord = _rotaAnasayifaRepository.Query().Any(u => u.RotaId == request.RotaId);

                //if (isThereRotaAnasayifaRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRotaAnasayifa = new RotaAnasayifa
                {
                    RotaId = request.RotaId,
                    Foto = request.Foto,
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Col = request.Col,
                    Yayin = request.Yayin,
                    Sira = request.Sira,

                };

                _rotaAnasayifaRepository.Add(addedRotaAnasayifa);
                await _rotaAnasayifaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}