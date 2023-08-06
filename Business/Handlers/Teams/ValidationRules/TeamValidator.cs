
using Business.Handlers.Teams.Commands;
using FluentValidation;

namespace Business.Handlers.Teams.ValidationRules
{

    public class CreateTeamValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Baslik).NotEmpty();
            RuleFor(x => x.Linkbir).NotEmpty();
            RuleFor(x => x.Linkiki).NotEmpty();
            RuleFor(x => x.Linkbuc).NotEmpty();

        }
    }
    public class UpdateTeamValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamValidator()
        {
            RuleFor(x => x.Foto).NotEmpty();
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Baslik).NotEmpty();
            RuleFor(x => x.Linkbir).NotEmpty();
            RuleFor(x => x.Linkiki).NotEmpty();
            RuleFor(x => x.Linkbuc).NotEmpty();

        }
    }
}