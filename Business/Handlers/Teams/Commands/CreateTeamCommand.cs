
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
using Business.Handlers.Teams.ValidationRules;

namespace Business.Handlers.Teams.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateTeamCommand : IRequest<IResult>
    {

        public string Foto { get; set; }
        public string Adi { get; set; }
        public string Baslik { get; set; }
        public string Linkbir { get; set; }
        public string Linkiki { get; set; }
        public string Linkbuc { get; set; }


        public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, IResult>
        {
            private readonly ITeamRepository _teamRepository;
            private readonly IMediator _mediator;
            public CreateTeamCommandHandler(ITeamRepository teamRepository, IMediator mediator)
            {
                _teamRepository = teamRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateTeamValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
            {
                //var isThereTeamRecord = _teamRepository.Query().Any(u => u.Foto == request.Foto);

                //if (isThereTeamRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedTeam = new Team
                {
                    Foto = request.Foto,
                    Adi = request.Adi,
                    Baslik = request.Baslik,
                    Linkbir = request.Linkbir,
                    Linkiki = request.Linkiki,
                    Linkbuc = request.Linkbuc,

                };

                _teamRepository.Add(addedTeam);
                await _teamRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}