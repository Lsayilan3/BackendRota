
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
using Business.Handlers.Desteks.ValidationRules;

namespace Business.Handlers.Desteks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDestekCommand : IRequest<IResult>
    {

        public string Foto { get; set; }


        public class CreateDestekCommandHandler : IRequestHandler<CreateDestekCommand, IResult>
        {
            private readonly IDestekRepository _destekRepository;
            private readonly IMediator _mediator;
            public CreateDestekCommandHandler(IDestekRepository destekRepository, IMediator mediator)
            {
                _destekRepository = destekRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDestekValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDestekCommand request, CancellationToken cancellationToken)
            {
                //var isThereDestekRecord = _destekRepository.Query().Any(u => u.Foto == request.Foto);

                //if (isThereDestekRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDestek = new Destek
                {
                    Foto = request.Foto,

                };

                _destekRepository.Add(addedDestek);
                await _destekRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}