
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
using Business.Handlers.Desteks.ValidationRules;


namespace Business.Handlers.Desteks.Commands
{


    public class UpdateDestekCommand : IRequest<IResult>
    {
        public int DestekId { get; set; }
        public string Foto { get; set; }

        public class UpdateDestekCommandHandler : IRequestHandler<UpdateDestekCommand, IResult>
        {
            private readonly IDestekRepository _destekRepository;
            private readonly IMediator _mediator;

            public UpdateDestekCommandHandler(IDestekRepository destekRepository, IMediator mediator)
            {
                _destekRepository = destekRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDestekValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDestekCommand request, CancellationToken cancellationToken)
            {
                var isThereDestekRecord = await _destekRepository.GetAsync(u => u.DestekId == request.DestekId);


                isThereDestekRecord.Foto = request.Foto;


                _destekRepository.Update(isThereDestekRecord);
                await _destekRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

