
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

namespace Business.Handlers.Kategoris.Queries
{

    public class GetKategorisQuery : IRequest<IDataResult<IEnumerable<Kategori>>>
    {
        public class GetKategorisQueryHandler : IRequestHandler<GetKategorisQuery, IDataResult<IEnumerable<Kategori>>>
        {
            private readonly IKategoriRepository _kategoriRepository;
            private readonly IMediator _mediator;

            public GetKategorisQueryHandler(IKategoriRepository kategoriRepository, IMediator mediator)
            {
                _kategoriRepository = kategoriRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Kategori>>> Handle(GetKategorisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Kategori>>(await _kategoriRepository.GetListAsync());
            }
        }
    }
}