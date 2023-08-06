﻿
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

using Microsoft.AspNetCore.Http;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Business.Handlers.Rotas.Queries;

namespace Business.Handlers.Rotas.Commands
{


    public class AddPhotoCommad : IRequest<IResult>
    {
        public int RotaId { get; set; }

        public IFormFile File { get; set; }

        public class AddPhotoCommadHandler : IRequestHandler<AddPhotoCommad, IResult>
        {
            private readonly IMediator _mediator;
            IHostingEnvironment _hostingEnvironment;
            public AddPhotoCommadHandler(IMediator mediator, IHostingEnvironment hostingEnvironment)
            {
                _mediator = mediator;
                _hostingEnvironment = hostingEnvironment;
            }


            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(AddPhotoCommad request, CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(new GetRotaQuery { RotaId = request.RotaId });
                if (request.File.Length > 0)
                {
                    string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/rota");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string filePath = Path.Combine(folderPath, request.File.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(fileStream);
                    }
                    result.Data.Foto = "/uploads/rota/" + request.File.FileName;
                    /*myClass.Photo = "/uploads/" + file.FileName; */
                    var upResult = await _mediator.Send(new UpdateRotaCommand()
                    {
                        RotaId = result.Data.RotaId,
                        Baslik = result.Data.Baslik,
                        Aciklama = result.Data.Aciklama,
                        Foto = result.Data.Foto,
                        Yayin = result.Data.Yayin,
                        Sira = result.Data.Sira,
                        Ozet = result.Data.Ozet,
                        KategoriId = result.Data.KategoriId,
                        SehirId = result.Data.SehirId,
                        AnaRotaId = result.Data.AnaRotaId,


                    });
                }
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
