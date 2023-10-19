
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.MediaFotoes.Queries
{

    public class GetMediaFotoesQuery : IRequest<IDataResult<IEnumerable<MediaFoto>>>
    {
        public class GetMediaFotoesQueryHandler : IRequestHandler<GetMediaFotoesQuery, IDataResult<IEnumerable<MediaFoto>>>
        {
            private readonly IMediaFotoRepository _mediaFotoRepository;
            private readonly IMediator _mediator;

            public GetMediaFotoesQueryHandler(IMediaFotoRepository mediaFotoRepository, IMediator mediator)
            {
                _mediaFotoRepository = mediaFotoRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<MediaFoto>>> Handle(GetMediaFotoesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<MediaFoto>>(await _mediaFotoRepository.GetListAsync());
            }
        }
    }
}