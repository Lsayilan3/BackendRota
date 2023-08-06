
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
using Business.Handlers.Kategoris.ValidationRules;

namespace Business.Handlers.Kategoris.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKategoriCommand : IRequest<IResult>
    {

        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Foto { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }


        public class CreateKategoriCommandHandler : IRequestHandler<CreateKategoriCommand, IResult>
        {
            private readonly IKategoriRepository _kategoriRepository;
            private readonly IMediator _mediator;
            public CreateKategoriCommandHandler(IKategoriRepository kategoriRepository, IMediator mediator)
            {
                _kategoriRepository = kategoriRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKategoriValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKategoriCommand request, CancellationToken cancellationToken)
            {
                var isThereKategoriRecord = _kategoriRepository.Query().Any(u => u.Baslik == request.Baslik);

                if (isThereKategoriRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKategori = new Kategori
                {
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Foto = request.Foto,
                    Yayin = request.Yayin,
                    Sira = request.Sira,

                };

                _kategoriRepository.Add(addedKategori);
                await _kategoriRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}