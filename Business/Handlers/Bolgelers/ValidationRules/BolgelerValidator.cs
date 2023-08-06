
using Business.Handlers.Bolgelers.Commands;
using FluentValidation;

namespace Business.Handlers.Bolgelers.ValidationRules
{

    public class CreateBolgelerValidator : AbstractValidator<CreateBolgelerCommand>
    {
        public CreateBolgelerValidator()
        {
            RuleFor(x => x.UlkeId).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
    public class UpdateBolgelerValidator : AbstractValidator<UpdateBolgelerCommand>
    {
        public UpdateBolgelerValidator()
        {
            RuleFor(x => x.UlkeId).NotEmpty();
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
        }
    }
}