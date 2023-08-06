
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
using Business.Handlers.Sehirs.ValidationRules;


namespace Business.Handlers.Sehirs.Commands
{


    public class UpdateSehirCommand : IRequest<IResult>
    {
        public int SehirId { get; set; }
        public int BolgelerId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }

        public class UpdateSehirCommandHandler : IRequestHandler<UpdateSehirCommand, IResult>
        {
            private readonly ISehirRepository _sehirRepository;
            private readonly IMediator _mediator;

            public UpdateSehirCommandHandler(ISehirRepository sehirRepository, IMediator mediator)
            {
                _sehirRepository = sehirRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSehirValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSehirCommand request, CancellationToken cancellationToken)
            {
                var isThereSehirRecord = await _sehirRepository.GetAsync(u => u.SehirId == request.SehirId);


                isThereSehirRecord.BolgelerId = request.BolgelerId;
                isThereSehirRecord.Foto = request.Foto;
                isThereSehirRecord.Baslik = request.Baslik;
                isThereSehirRecord.Aciklama = request.Aciklama;
                isThereSehirRecord.Yayin = request.Yayin;
                isThereSehirRecord.Sira = request.Sira;


                _sehirRepository.Update(isThereSehirRecord);
                await _sehirRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

