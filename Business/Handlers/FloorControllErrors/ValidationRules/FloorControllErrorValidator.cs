
using Business.Handlers.FloorControllErrors.Commands;
using FluentValidation;

namespace Business.Handlers.FloorControllErrors.ValidationRules
{

    public class CreateFloorControllErrorValidator : AbstractValidator<CreateFloorControllErrorCommand>
    {
        public CreateFloorControllErrorValidator()
        {
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Percent).NotEmpty();

        }
    }
    public class UpdateFloorControllErrorValidator : AbstractValidator<UpdateFloorControllErrorCommand>
    {
        public UpdateFloorControllErrorValidator()
        {
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Percent).NotEmpty();

        }
    }
}