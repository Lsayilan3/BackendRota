
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
using Business.Handlers.Ulkes.ValidationRules;


namespace Business.Handlers.Ulkes.Commands
{


    public class UpdateUlkeCommand : IRequest<IResult>
    {
        public int UlkeId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Foto { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }

        public class UpdateUlkeCommandHandler : IRequestHandler<UpdateUlkeCommand, IResult>
        {
            private readonly IUlkeRepository _ulkeRepository;
            private readonly IMediator _mediator;

            public UpdateUlkeCommandHandler(IUlkeRepository ulkeRepository, IMediator mediator)
            {
                _ulkeRepository = ulkeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateUlkeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateUlkeCommand request, CancellationToken cancellationToken)
            {
                var isThereUlkeRecord = await _ulkeRepository.GetAsync(u => u.UlkeId == request.UlkeId);


                isThereUlkeRecord.Baslik = request.Baslik;
                isThereUlkeRecord.Aciklama = request.Aciklama;
                isThereUlkeRecord.Foto = request.Foto;
                isThereUlkeRecord.Yayin = request.Yayin;
                isThereUlkeRecord.Sira = request.Sira;


                _ulkeRepository.Update(isThereUlkeRecord);
                await _ulkeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

