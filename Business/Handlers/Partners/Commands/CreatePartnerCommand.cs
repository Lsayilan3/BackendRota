
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
using Business.Handlers.Partners.ValidationRules;

namespace Business.Handlers.Partners.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePartnerCommand : IRequest<IResult>
    {

        public string Foto { get; set; }


        public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, IResult>
        {
            private readonly IPartnerRepository _partnerRepository;
            private readonly IMediator _mediator;
            public CreatePartnerCommandHandler(IPartnerRepository partnerRepository, IMediator mediator)
            {
                _partnerRepository = partnerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePartnerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
            {
                //var isTherePartnerRecord = _partnerRepository.Query().Any(u => u.Foto == request.Foto);

                //if (isTherePartnerRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPartner = new Partner
                {
                    Foto = request.Foto,

                };

                _partnerRepository.Add(addedPartner);
                await _partnerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}