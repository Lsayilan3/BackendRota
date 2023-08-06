
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
using Business.Handlers.Yorumlars.ValidationRules;


namespace Business.Handlers.Yorumlars.Commands
{


    public class UpdateYorumlarCommand : IRequest<IResult>
    {
        public int YorumlarId { get; set; }
        public int RotaId { get; set; }
        public int Puan { get; set; }
        public string Isim { get; set; }
        public string Baslik { get; set; }
        public string Yorum { get; set; }
        public int Yayin { get; set; }

        public class UpdateYorumlarCommandHandler : IRequestHandler<UpdateYorumlarCommand, IResult>
        {
            private readonly IYorumlarRepository _yorumlarRepository;
            private readonly IMediator _mediator;

            public UpdateYorumlarCommandHandler(IYorumlarRepository yorumlarRepository, IMediator mediator)
            {
                _yorumlarRepository = yorumlarRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateYorumlarValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateYorumlarCommand request, CancellationToken cancellationToken)
            {
                var isThereYorumlarRecord = await _yorumlarRepository.GetAsync(u => u.YorumlarId == request.YorumlarId);


                isThereYorumlarRecord.RotaId = request.RotaId;
                isThereYorumlarRecord.Puan = request.Puan;
                isThereYorumlarRecord.Isim = request.Isim;
                isThereYorumlarRecord.Baslik = request.Baslik;
                isThereYorumlarRecord.Yorum = request.Yorum;
                isThereYorumlarRecord.Yayin = request.Yayin;


                _yorumlarRepository.Update(isThereYorumlarRecord);
                await _yorumlarRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

