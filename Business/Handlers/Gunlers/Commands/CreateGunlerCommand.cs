
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
using Business.Handlers.Gunlers.ValidationRules;

namespace Business.Handlers.Gunlers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateGunlerCommand : IRequest<IResult>
    {
        public int RotaId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }


        public class CreateGunlerCommandHandler : IRequestHandler<CreateGunlerCommand, IResult>
        {
            private readonly IGunlerRepository _gunlerRepository;
            private readonly IMediator _mediator;
            public CreateGunlerCommandHandler(IGunlerRepository gunlerRepository, IMediator mediator)
            {
                _gunlerRepository = gunlerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGunlerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGunlerCommand request, CancellationToken cancellationToken)
            {
                //var isThereGunlerRecord = _gunlerRepository.Query().Any(u => u.Baslik == request.Baslik);

                //if (isThereGunlerRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedGunler = new Gunler
                {
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    RotaId = request.RotaId,

                };

                _gunlerRepository.Add(addedGunler);
                await _gunlerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}