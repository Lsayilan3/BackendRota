
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
using Business.Handlers.MediaFotoes.ValidationRules;


namespace Business.Handlers.MediaFotoes.Commands
{


    public class UpdateMediaFotoCommand : IRequest<IResult>
    {
        public int MediaFotoId { get; set; }
        public string Foto { get; set; }

        public class UpdateMediaFotoCommandHandler : IRequestHandler<UpdateMediaFotoCommand, IResult>
        {
            private readonly IMediaFotoRepository _mediaFotoRepository;
            private readonly IMediator _mediator;

            public UpdateMediaFotoCommandHandler(IMediaFotoRepository mediaFotoRepository, IMediator mediator)
            {
                _mediaFotoRepository = mediaFotoRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateMediaFotoValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateMediaFotoCommand request, CancellationToken cancellationToken)
            {
                var isThereMediaFotoRecord = await _mediaFotoRepository.GetAsync(u => u.MediaFotoId == request.MediaFotoId);


                isThereMediaFotoRecord.Foto = request.Foto;


                _mediaFotoRepository.Update(isThereMediaFotoRecord);
                await _mediaFotoRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

