
using Business.Handlers.Yorumlars.Commands;
using FluentValidation;

namespace Business.Handlers.Yorumlars.ValidationRules
{

    public class CreateYorumlarValidator : AbstractValidator<CreateYorumlarCommand>
    {
        public CreateYorumlarValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Puan).NotEmpty();
            //RuleFor(x => x.Isim).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Yorum).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();

        }
    }
    public class UpdateYorumlarValidator : AbstractValidator<UpdateYorumlarCommand>
    {
        public UpdateYorumlarValidator()
        {
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Puan).NotEmpty();
            //RuleFor(x => x.Isim).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Yorum).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();

        }
    }
}