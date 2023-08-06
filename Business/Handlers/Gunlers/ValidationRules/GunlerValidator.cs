
using Business.Handlers.Gunlers.Commands;
using FluentValidation;

namespace Business.Handlers.Gunlers.ValidationRules
{

    public class CreateGunlerValidator : AbstractValidator<CreateGunlerCommand>
    {
        public CreateGunlerValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);

        }
    }
    public class UpdateGunlerValidator : AbstractValidator<UpdateGunlerCommand>
    {
        public UpdateGunlerValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);

        }
    }
}