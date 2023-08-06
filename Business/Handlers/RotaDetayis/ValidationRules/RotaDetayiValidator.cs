
using Business.Handlers.RotaDetayis.Commands;
using FluentValidation;

namespace Business.Handlers.RotaDetayis.ValidationRules
{

    public class CreateRotaDetayiValidator : AbstractValidator<CreateRotaDetayiCommand>
    {
        public CreateRotaDetayiValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Ozet).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.KategoriId).NotEmpty();
            RuleFor(x => x.SehirId).NotEmpty();

        }
    }
    public class UpdateRotaDetayiValidator : AbstractValidator<UpdateRotaDetayiCommand>
    {
        public UpdateRotaDetayiValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Ozet).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.KategoriId).NotEmpty();
            RuleFor(x => x.SehirId).NotEmpty();

        }
    }
}