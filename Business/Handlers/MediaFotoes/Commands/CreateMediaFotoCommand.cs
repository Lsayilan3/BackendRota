
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
using Business.Handlers.MediaFotoes.ValidationRules;

namespace Business.Handlers.MediaFotoes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMediaFotoCommand : IRequest<IResult>
    {

        public string Foto { get; set; }


        public class CreateMediaFotoCommandHandler : IRequestHandler<CreateMediaFotoCommand, IResult>
        {
            private readonly IMediaFotoRepository _mediaFotoRepository;
            private readonly IMediator _mediator;
            public CreateMediaFotoCommandHandler(IMediaFotoRepository mediaFotoRepository, IMediator mediator)
            {
                _mediaFotoRepository = mediaFotoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateMediaFotoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateMediaFotoCommand request, CancellationToken cancellationToken)
            {
                var isThereMediaFotoRecord = _mediaFotoRepository.Query().Any(u => u.Foto == request.Foto);

                if (isThereMediaFotoRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedMediaFoto = new MediaFoto
                {
                    Foto = request.Foto,

                };

                _mediaFotoRepository.Add(addedMediaFoto);
                await _mediaFotoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}