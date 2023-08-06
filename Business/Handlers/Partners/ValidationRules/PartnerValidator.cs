
using Business.Handlers.Partners.Commands;
using FluentValidation;

namespace Business.Handlers.Partners.ValidationRules
{

    public class CreatePartnerValidator : AbstractValidator<CreatePartnerCommand>
    {
        public CreatePartnerValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
    public class UpdatePartnerValidator : AbstractValidator<UpdatePartnerCommand>
    {
        public UpdatePartnerValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
}