
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
using Business.Handlers.Bolgelers.ValidationRules;

namespace Business.Handlers.Bolgelers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBolgelerCommand : IRequest<IResult>
    {

        public int UlkeId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }


        public class CreateBolgelerCommandHandler : IRequestHandler<CreateBolgelerCommand, IResult>
        {
            private readonly IBolgelerRepository _bolgelerRepository;
            private readonly IMediator _mediator;
            public CreateBolgelerCommandHandler(IBolgelerRepository bolgelerRepository, IMediator mediator)
            {
                _bolgelerRepository = bolgelerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBolgelerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBolgelerCommand request, CancellationToken cancellationToken)
            {
                //var isThereBolgelerRecord = _bolgelerRepository.Query().Any(u => u.UlkeId == request.UlkeId);

                //if (isThereBolgelerRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBolgeler = new Bolgeler
                {
                    UlkeId = request.UlkeId,
                    Foto = request.Foto,
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Yayin = request.Yayin,
                    Sira = request.Sira,

                };

                _bolgelerRepository.Add(addedBolgeler);
                await _bolgelerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}