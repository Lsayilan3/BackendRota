
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Kategoris.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKategoriCommand : IRequest<IResult>
    {
        public int KategoriId { get; set; }

        public class DeleteKategoriCommandHandler : IRequestHandler<DeleteKategoriCommand, IResult>
        {
            private readonly IKategoriRepository _kategoriRepository;
            private readonly IMediator _mediator;

            public DeleteKategoriCommandHandler(IKategoriRepository kategoriRepository, IMediator mediator)
            {
                _kategoriRepository = kategoriRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKategoriCommand request, CancellationToken cancellationToken)
            {
                var kategoriToDelete = _kategoriRepository.Get(p => p.KategoriId == request.KategoriId);

                _kategoriRepository.Delete(kategoriToDelete);
                await _kategoriRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

