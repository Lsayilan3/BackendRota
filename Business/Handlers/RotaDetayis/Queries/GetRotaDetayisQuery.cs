
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

namespace Business.Handlers.RotaDetayis.Queries
{

    public class GetRotaDetayisQuery : IRequest<IDataResult<IEnumerable<RotaDetayi>>>
    {
        public class GetRotaDetayisQueryHandler : IRequestHandler<GetRotaDetayisQuery, IDataResult<IEnumerable<RotaDetayi>>>
        {
            private readonly IRotaDetayiRepository _rotaDetayiRepository;
            private readonly IMediator _mediator;

            public GetRotaDetayisQueryHandler(IRotaDetayiRepository rotaDetayiRepository, IMediator mediator)
            {
                _rotaDetayiRepository = rotaDetayiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RotaDetayi>>> Handle(GetRotaDetayisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RotaDetayi>>(await _rotaDetayiRepository.GetListAsync());
            }
        }
    }
}