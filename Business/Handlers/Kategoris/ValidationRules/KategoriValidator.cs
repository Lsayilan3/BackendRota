
using Business.Handlers.Kategoris.Commands;
using FluentValidation;

namespace Business.Handlers.Kategoris.ValidationRules
{

    public class CreateKategoriValidator : AbstractValidator<CreateKategoriCommand>
    {
        public CreateKategoriValidator()
        {
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
    public class UpdateKategoriValidator : AbstractValidator<UpdateKategoriCommand>
    {
        public UpdateKategoriValidator()
        {
            //RuleFor(x => x.Baslik).MaximumLength(1000000000);
            //RuleFor(x => x.Aciklama).MaximumLength(1000000000);
            //RuleFor(x => x.Foto).MaximumLength(1000000000);
            //RuleFor(x => x.Yayin).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
}