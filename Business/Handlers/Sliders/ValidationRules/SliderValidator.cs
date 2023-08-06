﻿
using Business.Handlers.Sliders.Commands;
using FluentValidation;

namespace Business.Handlers.Sliders.ValidationRules
{

    public class CreateSliderValidator : AbstractValidator<CreateSliderCommand>
    {
        public CreateSliderValidator()
        {
            RuleFor(x => x.Altyazi).NotEmpty();
            RuleFor(x => x.Baslik).NotEmpty();
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
    public class UpdateSliderValidator : AbstractValidator<UpdateSliderCommand>
    {
        public UpdateSliderValidator()
        {
            RuleFor(x => x.Altyazi).NotEmpty();
            RuleFor(x => x.Baslik).NotEmpty();
            RuleFor(x => x.Foto).NotEmpty();

        }
    }
}