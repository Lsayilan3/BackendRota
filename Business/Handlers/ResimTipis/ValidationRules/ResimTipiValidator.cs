
using Business.Handlers.ResimTipis.Commands;
using FluentValidation;

namespace Business.Handlers.ResimTipis.ValidationRules
{

    public class CreateResimTipiValidator : AbstractValidator<CreateResimTipiCommand>
    {
        public CreateResimTipiValidator()
        {
            //RuleFor(x => x.Adi).MaximumLength(1000000000);

        }
    }
    public class UpdateResimTipiValidator : AbstractValidator<UpdateResimTipiCommand>
    {
        public UpdateResimTipiValidator()
        {
            //RuleFor(x => x.Adi).MaximumLength(1000000000);

        }
    }
}