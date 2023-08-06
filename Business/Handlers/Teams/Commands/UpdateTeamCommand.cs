
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
using Business.Handlers.Teams.ValidationRules;


namespace Business.Handlers.Teams.Commands
{


    public class UpdateTeamCommand : IRequest<IResult>
    {
        public int TeamId { get; set; }
        public string Foto { get; set; }
        public string Adi { get; set; }
        public string Baslik { get; set; }
        public string Linkbir { get; set; }
        public string Linkiki { get; set; }
        public string Linkbuc { get; set; }

        public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, IResult>
        {
            private readonly ITeamRepository _teamRepository;
            private readonly IMediator _mediator;

            public UpdateTeamCommandHandler(ITeamRepository teamRepository, IMediator mediator)
            {
                _teamRepository = teamRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateTeamValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
            {
                var isThereTeamRecord = await _teamRepository.GetAsync(u => u.TeamId == request.TeamId);


                isThereTeamRecord.Foto = request.Foto;
                isThereTeamRecord.Adi = request.Adi;
                isThereTeamRecord.Baslik = request.Baslik;
                isThereTeamRecord.Linkbir = request.Linkbir;
                isThereTeamRecord.Linkiki = request.Linkiki;
                isThereTeamRecord.Linkbuc = request.Linkbuc;


                _teamRepository.Update(isThereTeamRecord);
                await _teamRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

