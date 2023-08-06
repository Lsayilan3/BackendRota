
using Business.Handlers.Sehirs.Commands;
using FluentValidation;

namespace Business.Handlers.Sehirs.ValidationRules
{

    public class CreateSehirValidator : AbstractValidator<CreateSehirCommand>
    {
        public CreateSehirValidator()
        {
            RuleFor(x => x.BolgelerId).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
    public class UpdateSehirValidator : AbstractValidator<UpdateSehirCommand>
    {
        public UpdateSehirValidator()
        {
            RuleFor(x => x.BolgelerId).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
}