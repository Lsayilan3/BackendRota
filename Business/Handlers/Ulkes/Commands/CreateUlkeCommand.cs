
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
using Business.Handlers.Ulkes.ValidationRules;

namespace Business.Handlers.Ulkes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateUlkeCommand : IRequest<IResult>
    {

        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Foto { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }


        public class CreateUlkeCommandHandler : IRequestHandler<CreateUlkeCommand, IResult>
        {
            private readonly IUlkeRepository _ulkeRepository;
            private readonly IMediator _mediator;
            public CreateUlkeCommandHandler(IUlkeRepository ulkeRepository, IMediator mediator)
            {
                _ulkeRepository = ulkeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateUlkeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateUlkeCommand request, CancellationToken cancellationToken)
            {
                //var isThereUlkeRecord = _ulkeRepository.Query().Any(u => u.Baslik == request.Baslik);

                //if (isThereUlkeRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedUlke = new Ulke
                {
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Foto = request.Foto,
                    Yayin = request.Yayin,
                    Sira = request.Sira,

                };

                _ulkeRepository.Add(addedUlke);
                await _ulkeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}