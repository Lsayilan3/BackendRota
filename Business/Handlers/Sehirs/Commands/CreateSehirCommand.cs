
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
using Business.Handlers.Sehirs.ValidationRules;

namespace Business.Handlers.Sehirs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSehirCommand : IRequest<IResult>
    {

        public int BolgelerId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }


        public class CreateSehirCommandHandler : IRequestHandler<CreateSehirCommand, IResult>
        {
            private readonly ISehirRepository _sehirRepository;
            private readonly IMediator _mediator;
            public CreateSehirCommandHandler(ISehirRepository sehirRepository, IMediator mediator)
            {
                _sehirRepository = sehirRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSehirValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSehirCommand request, CancellationToken cancellationToken)
            {
                //var isThereSehirRecord = _sehirRepository.Query().Any(u => u.BolgelerId == request.BolgelerId);

                //if (isThereSehirRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSehir = new Sehir
                {
                    BolgelerId = request.BolgelerId,
                    Foto = request.Foto,
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Yayin = request.Yayin,
                    Sira = request.Sira,

                };

                _sehirRepository.Add(addedSehir);
                await _sehirRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}