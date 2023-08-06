
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Kategoris.Queries
{
    public class GetKategoriQuery : IRequest<IDataResult<Kategori>>
    {
        public int KategoriId { get; set; }

        public class GetKategoriQueryHandler : IRequestHandler<GetKategoriQuery, IDataResult<Kategori>>
        {
            private readonly IKategoriRepository _kategoriRepository;
            private readonly IMediator _mediator;

            public GetKategoriQueryHandler(IKategoriRepository kategoriRepository, IMediator mediator)
            {
                _kategoriRepository = kategoriRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Kategori>> Handle(GetKategoriQuery request, CancellationToken cancellationToken)
            {
                var kategori = await _kategoriRepository.GetAsync(p => p.KategoriId == request.KategoriId);
                return new SuccessDataResult<Kategori>(kategori);
            }
        }
    }
}
