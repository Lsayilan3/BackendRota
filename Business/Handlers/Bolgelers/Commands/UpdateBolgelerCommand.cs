
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
using Business.Handlers.Bolgelers.ValidationRules;


namespace Business.Handlers.Bolgelers.Commands
{


    public class UpdateBolgelerCommand : IRequest<IResult>
    {
        public int BolgelerId { get; set; }
        public int UlkeId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }

        public class UpdateBolgelerCommandHandler : IRequestHandler<UpdateBolgelerCommand, IResult>
        {
            private readonly IBolgelerRepository _bolgelerRepository;
            private readonly IMediator _mediator;

            public UpdateBolgelerCommandHandler(IBolgelerRepository bolgelerRepository, IMediator mediator)
            {
                _bolgelerRepository = bolgelerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBolgelerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBolgelerCommand request, CancellationToken cancellationToken)
            {
                var isThereBolgelerRecord = await _bolgelerRepository.GetAsync(u => u.BolgelerId == request.BolgelerId);


                isThereBolgelerRecord.UlkeId = request.UlkeId;
                isThereBolgelerRecord.Foto = request.Foto;
                isThereBolgelerRecord.Baslik = request.Baslik;
                isThereBolgelerRecord.Aciklama = request.Aciklama;
                isThereBolgelerRecord.Yayin = request.Yayin;
                isThereBolgelerRecord.Sira = request.Sira;


                _bolgelerRepository.Update(isThereBolgelerRecord);
                await _bolgelerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

