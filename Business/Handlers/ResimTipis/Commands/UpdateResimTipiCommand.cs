
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
using Business.Handlers.ResimTipis.ValidationRules;


namespace Business.Handlers.ResimTipis.Commands
{


    public class UpdateResimTipiCommand : IRequest<IResult>
    {
        public int ResimTipiId { get; set; }
        public string Adi { get; set; }

        public class UpdateResimTipiCommandHandler : IRequestHandler<UpdateResimTipiCommand, IResult>
        {
            private readonly IResimTipiRepository _resimTipiRepository;
            private readonly IMediator _mediator;

            public UpdateResimTipiCommandHandler(IResimTipiRepository resimTipiRepository, IMediator mediator)
            {
                _resimTipiRepository = resimTipiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateResimTipiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateResimTipiCommand request, CancellationToken cancellationToken)
            {
                var isThereResimTipiRecord = await _resimTipiRepository.GetAsync(u => u.ResimTipiId == request.ResimTipiId);


                isThereResimTipiRecord.Adi = request.Adi;


                _resimTipiRepository.Update(isThereResimTipiRecord);
                await _resimTipiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

