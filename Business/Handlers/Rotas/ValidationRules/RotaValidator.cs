
using Business.Handlers.Rotas.Commands;
using FluentValidation;

namespace Business.Handlers.Rotas.ValidationRules
{

    public class CreateRotaValidator : AbstractValidator<CreateRotaCommand>
    {
        public CreateRotaValidator()
        {
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Ozet).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.KategoriId).NotEmpty();
            RuleFor(x => x.SehirId).NotEmpty();
            //RuleFor(x => x.AnaRotaId).NotEmpty();

        }
    }
    public class UpdateRotaValidator : AbstractValidator<UpdateRotaCommand>
    {
        public UpdateRotaValidator()
        {
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Ozet).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.KategoriId).NotEmpty();
            RuleFor(x => x.SehirId).NotEmpty();
            //RuleFor(x => x.AnaRotaId).NotEmpty();


        }
    }
}