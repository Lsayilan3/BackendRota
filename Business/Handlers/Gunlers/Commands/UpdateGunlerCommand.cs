
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
using Business.Handlers.Gunlers.ValidationRules;


namespace Business.Handlers.Gunlers.Commands
{


    public class UpdateGunlerCommand : IRequest<IResult>
    {
        public int GunlerId { get; set; }
        public int RotaId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

        public class UpdateGunlerCommandHandler : IRequestHandler<UpdateGunlerCommand, IResult>
        {
            private readonly IGunlerRepository _gunlerRepository;
            private readonly IMediator _mediator;

            public UpdateGunlerCommandHandler(IGunlerRepository gunlerRepository, IMediator mediator)
            {
                _gunlerRepository = gunlerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGunlerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGunlerCommand request, CancellationToken cancellationToken)
            {
                var isThereGunlerRecord = await _gunlerRepository.GetAsync(u => u.GunlerId == request.GunlerId);


                isThereGunlerRecord.Baslik = request.Baslik;
                isThereGunlerRecord.Aciklama = request.Aciklama;
                isThereGunlerRecord.RotaId = request.RotaId;


                _gunlerRepository.Update(isThereGunlerRecord);
                await _gunlerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

