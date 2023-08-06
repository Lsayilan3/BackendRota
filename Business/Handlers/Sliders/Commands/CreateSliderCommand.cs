
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
using Business.Handlers.Sliders.ValidationRules;

namespace Business.Handlers.Sliders.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSliderCommand : IRequest<IResult>
    {

        public string Altyazi { get; set; }
        public string Baslik { get; set; }
        public string Foto { get; set; }


        public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommand, IResult>
        {
            private readonly ISliderRepository _sliderRepository;
            private readonly IMediator _mediator;
            public CreateSliderCommandHandler(ISliderRepository sliderRepository, IMediator mediator)
            {
                _sliderRepository = sliderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSliderValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
            {
                //var isThereSliderRecord = _sliderRepository.Query().Any(u => u.Altyazi == request.Altyazi);

                //if (isThereSliderRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSlider = new Slider
                {
                    Altyazi = request.Altyazi,
                    Baslik = request.Baslik,
                    Foto = request.Foto,

                };

                _sliderRepository.Add(addedSlider);
                await _sliderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}