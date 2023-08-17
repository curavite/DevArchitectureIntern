
using Business.Handlers.WashingControll_FloorControlls.Commands;
using FluentValidation;

namespace Business.Handlers.WashingControll_FloorControlls.ValidationRules
{

    public class CreateWashingControll_FloorControllValidator : AbstractValidator<CreateWashingControll_FloorControllCommand>
    {
        public CreateWashingControll_FloorControllValidator()
        {
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Percent).NotEmpty();

            //RuleFor(x => x.FaultyProduct).NotEmpty();
            //RuleFor(x => x.ControllTime).NotEmpty();
            //RuleFor(x => x.ControllResult).NotEmpty();
            //RuleFor(x => x.ManagerReview).NotEmpty();
            //RuleFor(x => x.PercentResult).NotEmpty();

        }
    }
    public class UpdateWashingControll_FloorControllValidator : AbstractValidator<UpdateWashingControll_FloorControllCommand>
    {
        public UpdateWashingControll_FloorControllValidator()
        {
            //RuleFor(x => x.Amount).NotEmpty();
            //RuleFor(x => x.Percent).NotEmpty();
            RuleFor(x => x.FaultyProduct).NotEmpty();
            RuleFor(x => x.ControllTime).NotEmpty();
            RuleFor(x => x.ControllResult).NotEmpty();
            RuleFor(x => x.ManagerReview).NotEmpty();
            RuleFor(x => x.PercentResult).NotEmpty();

        }
    }
}