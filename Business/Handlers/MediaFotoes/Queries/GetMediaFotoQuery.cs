
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.MediaFotoes.Queries
{
    public class GetMediaFotoQuery : IRequest<IDataResult<MediaFoto>>
    {
        public int MediaFotoId { get; set; }

        public class GetMediaFotoQueryHandler : IRequestHandler<GetMediaFotoQuery, IDataResult<MediaFoto>>
        {
            private readonly IMediaFotoRepository _mediaFotoRepository;
            private readonly IMediator _mediator;

            public GetMediaFotoQueryHandler(IMediaFotoRepository mediaFotoRepository, IMediator mediator)
            {
                _mediaFotoRepository = mediaFotoRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<MediaFoto>> Handle(GetMediaFotoQuery request, CancellationToken cancellationToken)
            {
                var mediaFoto = await _mediaFotoRepository.GetAsync(p => p.MediaFotoId == request.MediaFotoId);
                return new SuccessDataResult<MediaFoto>(mediaFoto);
            }
        }
    }
}
