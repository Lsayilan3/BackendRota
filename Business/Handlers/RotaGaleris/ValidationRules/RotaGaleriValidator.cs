
using Business.Handlers.RotaGaleris.Commands;
using FluentValidation;

namespace Business.Handlers.RotaGaleris.ValidationRules
{

    public class CreateRotaGaleriValidator : AbstractValidator<CreateRotaGaleriCommand>
    {
        public CreateRotaGaleriValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.ResimTipiId).NotEmpty();


        }
    }
    public class UpdateRotaGaleriValidator : AbstractValidator<UpdateRotaGaleriCommand>
    {
        public UpdateRotaGaleriValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.ResimTipiId).NotEmpty();

        }
    }
}