
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

namespace Business.Handlers.RotaGaleris.Queries
{

    public class GetRotaGaleriListByRotaId : IRequest<IDataResult<IEnumerable<RotaGaleri>>>
    {
        public int RotaId { get; set; }

        public class GetRotaDetayisQueryHandler : IRequestHandler<GetRotaGaleriListByRotaId, IDataResult<IEnumerable<RotaGaleri>>>
        {
            private readonly IRotaGaleriRepository _rotaGaleriRepository;
            private readonly IMediator _mediator;

            public GetRotaDetayisQueryHandler(IRotaGaleriRepository rotaGaleriRepository, IMediator mediator)
            {
                _rotaGaleriRepository = rotaGaleriRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RotaGaleri>>> Handle(GetRotaGaleriListByRotaId request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RotaGaleri>>(await _rotaGaleriRepository.GetListAsync(x => x.RotaId == request.RotaId));
            }
        }
    }
}
