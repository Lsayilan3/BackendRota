
using Business.Handlers.Puans.Commands;
using FluentValidation;

namespace Business.Handlers.Puans.ValidationRules
{

    public class CreatePuanValidator : AbstractValidator<CreatePuanCommand>
    {
        public CreatePuanValidator()
        {
            //RuleFor(x => x.GenelPuan).MaximumLength(1000000000);
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Hizmetler).MaximumLength(1000000000);
            //RuleFor(x => x.HizmetlerPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Konum).MaximumLength(1000000000);
            //RuleFor(x => x.KonumPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Kolayliklar).MaximumLength(1000000000);
            //RuleFor(x => x.KolayliklarPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Fiyat).MaximumLength(1000000000);
            //RuleFor(x => x.FiyatPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Yiyecek).MaximumLength(1000000000);
            //RuleFor(x => x.YiyecekPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Harita).MaximumLength(1000000000);

        }
    }
    public class UpdatePuanValidator : AbstractValidator<UpdatePuanCommand>
    {
        public UpdatePuanValidator()
        {
            //RuleFor(x => x.GenelPuan).MaximumLength(1000000000);
            RuleFor(x => x.RotaId).NotEmpty();
            //RuleFor(x => x.Hizmetler).MaximumLength(1000000000);
            //RuleFor(x => x.HizmetlerPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Konum).MaximumLength(1000000000);
            //RuleFor(x => x.KonumPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Kolayliklar).MaximumLength(1000000000);
            //RuleFor(x => x.KolayliklarPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Fiyat).MaximumLength(1000000000);
            //RuleFor(x => x.FiyatPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Yiyecek).MaximumLength(1000000000);
            //RuleFor(x => x.YiyecekPuan).MaximumLength(1000000000);
            //RuleFor(x => x.Harita).MaximumLength(1000000000);


        }
    }
}