
using Business.Handlers.Ulkes.Commands;
using FluentValidation;

namespace Business.Handlers.Ulkes.ValidationRules
{

    public class CreateUlkeValidator : AbstractValidator<CreateUlkeCommand>
    {
        public CreateUlkeValidator()
        {
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
    public class UpdateUlkeValidator : AbstractValidator<UpdateUlkeCommand>
    {
        public UpdateUlkeValidator()
        {
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
}