
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
using Business.Handlers.ResimTipis.ValidationRules;

namespace Business.Handlers.ResimTipis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateResimTipiCommand : IRequest<IResult>
    {

        public string Adi { get; set; }


        public class CreateResimTipiCommandHandler : IRequestHandler<CreateResimTipiCommand, IResult>
        {
            private readonly IResimTipiRepository _resimTipiRepository;
            private readonly IMediator _mediator;
            public CreateResimTipiCommandHandler(IResimTipiRepository resimTipiRepository, IMediator mediator)
            {
                _resimTipiRepository = resimTipiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateResimTipiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateResimTipiCommand request, CancellationToken cancellationToken)
            {
                var isThereResimTipiRecord = _resimTipiRepository.Query().Any(u => u.Adi == request.Adi);

                if (isThereResimTipiRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedResimTipi = new ResimTipi
                {
                    Adi = request.Adi,

                };

                _resimTipiRepository.Add(addedResimTipi);
                await _resimTipiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}