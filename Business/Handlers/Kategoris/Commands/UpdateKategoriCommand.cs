
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
using Business.Handlers.Kategoris.ValidationRules;


namespace Business.Handlers.Kategoris.Commands
{


    public class UpdateKategoriCommand : IRequest<IResult>
    {
        public int KategoriId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Foto { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }

        public class UpdateKategoriCommandHandler : IRequestHandler<UpdateKategoriCommand, IResult>
        {
            private readonly IKategoriRepository _kategoriRepository;
            private readonly IMediator _mediator;

            public UpdateKategoriCommandHandler(IKategoriRepository kategoriRepository, IMediator mediator)
            {
                _kategoriRepository = kategoriRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKategoriValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKategoriCommand request, CancellationToken cancellationToken)
            {
                var isThereKategoriRecord = await _kategoriRepository.GetAsync(u => u.KategoriId == request.KategoriId);


                isThereKategoriRecord.Baslik = request.Baslik;
                isThereKategoriRecord.Aciklama = request.Aciklama;
                isThereKategoriRecord.Foto = request.Foto;
                isThereKategoriRecord.Yayin = request.Yayin;
                isThereKategoriRecord.Sira = request.Sira;


                _kategoriRepository.Update(isThereKategoriRecord);
                await _kategoriRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

