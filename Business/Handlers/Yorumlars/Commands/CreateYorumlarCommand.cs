
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
using Business.Handlers.Yorumlars.ValidationRules;

namespace Business.Handlers.Yorumlars.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateYorumlarCommand : IRequest<IResult>
    {

        public int RotaId { get; set; }
        public int Puan { get; set; }
        public string Isim { get; set; }
        public string Baslik { get; set; }
        public string Yorum { get; set; }
        public int Yayin { get; set; }


        public class CreateYorumlarCommandHandler : IRequestHandler<CreateYorumlarCommand, IResult>
        {
            private readonly IYorumlarRepository _yorumlarRepository;
            private readonly IMediator _mediator;
            public CreateYorumlarCommandHandler(IYorumlarRepository yorumlarRepository, IMediator mediator)
            {
                _yorumlarRepository = yorumlarRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateYorumlarValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateYorumlarCommand request, CancellationToken cancellationToken)
            {
                //var isThereYorumlarRecord = _yorumlarRepository.Query().Any(u => u.RotaId == request.RotaId);

                //if (isThereYorumlarRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedYorumlar = new Yorumlar
                {
                    RotaId = request.RotaId,
                    Puan = request.Puan,
                    Isim = request.Isim,
                    Baslik = request.Baslik,
                    Yorum = request.Yorum,
                    Yayin = request.Yayin,

                };

                _yorumlarRepository.Add(addedYorumlar);
                await _yorumlarRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}