
using Business.Handlers.MediaFotoes.Commands;
using FluentValidation;

namespace Business.Handlers.MediaFotoes.ValidationRules
{

    public class CreateMediaFotoValidator : AbstractValidator<CreateMediaFotoCommand>
    {
        public CreateMediaFotoValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
    public class UpdateMediaFotoValidator : AbstractValidator<UpdateMediaFotoCommand>
    {
        public UpdateMediaFotoValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
}