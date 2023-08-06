
using Business.Handlers.Desteks.Commands;
using FluentValidation;

namespace Business.Handlers.Desteks.ValidationRules
{

    public class CreateDestekValidator : AbstractValidator<CreateDestekCommand>
    {
        public CreateDestekValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
    public class UpdateDestekValidator : AbstractValidator<UpdateDestekCommand>
    {
        public UpdateDestekValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
}