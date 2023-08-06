
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
using Business.Handlers.Partners.ValidationRules;


namespace Business.Handlers.Partners.Commands
{


    public class UpdatePartnerCommand : IRequest<IResult>
    {
        public int PartnerId { get; set; }
        public string Foto { get; set; }

        public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand, IResult>
        {
            private readonly IPartnerRepository _partnerRepository;
            private readonly IMediator _mediator;

            public UpdatePartnerCommandHandler(IPartnerRepository partnerRepository, IMediator mediator)
            {
                _partnerRepository = partnerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePartnerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
            {
                var isTherePartnerRecord = await _partnerRepository.GetAsync(u => u.PartnerId == request.PartnerId);


                isTherePartnerRecord.Foto = request.Foto;


                _partnerRepository.Update(isTherePartnerRecord);
                await _partnerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

