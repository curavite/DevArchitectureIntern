
using Business.Handlers.FloorControllErrors.Commands;
using FluentValidation;

namespace Business.Handlers.FloorControllErrors.ValidationRules
{

    public class CreateFloorControllErrorValidator : AbstractValidator<CreateFloorControllErrorCommand>
    {
        public CreateFloorControllErrorValidator()
        {
            RuleFor(x => x.ErrorName).NotEmpty();

            RuleFor(x => x.Amount).NotEmpty();

        }
    }
    public class UpdateFloorControllErrorValidator : AbstractValidator<UpdateFloorControllErrorCommand>
    {
        public UpdateFloorControllErrorValidator()
        {
            RuleFor(x => x.Amount).NotEmpty();

        }
    }
}