
using Business.Handlers.RotaAnasayifas.Commands;
using FluentValidation;

namespace Business.Handlers.RotaAnasayifas.ValidationRules
{

    public class CreateRotaAnasayifaValidator : AbstractValidator<CreateRotaAnasayifaCommand>
    {
        public CreateRotaAnasayifaValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Foto).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Kol).NotEmpty();
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
    public class UpdateRotaAnasayifaValidator : AbstractValidator<UpdateRotaAnasayifaCommand>
    {
        public UpdateRotaAnasayifaValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Foto).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Kol).NotEmpty();
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
}