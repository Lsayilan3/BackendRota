
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


namespace Business.Handlers.Yorumlars.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteYorumlarCommand : IRequest<IResult>
    {
        public int YorumlarId { get; set; }

        public class DeleteYorumlarCommandHandler : IRequestHandler<DeleteYorumlarCommand, IResult>
        {
            private readonly IYorumlarRepository _yorumlarRepository;
            private readonly IMediator _mediator;

            public DeleteYorumlarCommandHandler(IYorumlarRepository yorumlarRepository, IMediator mediator)
            {
                _yorumlarRepository = yorumlarRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteYorumlarCommand request, CancellationToken cancellationToken)
            {
                var yorumlarToDelete = _yorumlarRepository.Get(p => p.YorumlarId == request.YorumlarId);

                _yorumlarRepository.Delete(yorumlarToDelete);
                await _yorumlarRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

