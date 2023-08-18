
using Business.Handlers.WashingControll_Floors.Commands;
using FluentValidation;

namespace Business.Handlers.WashingControll_Floors.ValidationRules
{

    public class CreateWashingControll_FloorValidator : AbstractValidator<CreateWashingControll_FloorCommand>
    {
        public CreateWashingControll_FloorValidator()
        {
            RuleFor(x => x.WashingMachine).NotEmpty();
            RuleFor(x => x.DryingMachine).NotEmpty();
            RuleFor(x => x.SqueezMachine).NotEmpty();
            RuleFor(x => x.MachineEmployee).NotEmpty();
            RuleFor(x => x.ManagerName).NotEmpty();
            RuleFor(x => x.SumProductAmount).NotEmpty();
            RuleFor(x => x.BrendaNumber).NotEmpty();
            RuleFor(x => x.jobRotation).NotEmpty();

        }
    }
    public class UpdateWashingControll_FloorValidator : AbstractValidator<UpdateWashingControll_FloorCommand>
    {
        public UpdateWashingControll_FloorValidator()
        {
            RuleFor(x => x.WashingMachine).NotEmpty();
            RuleFor(x => x.DryingMachine).NotEmpty();
            RuleFor(x => x.SqueezMachine).NotEmpty();
            RuleFor(x => x.MachineEmployee).NotEmpty();
            RuleFor(x => x.ManagerName).NotEmpty();
            RuleFor(x => x.SumProductAmount).NotEmpty();
            RuleFor(x => x.BrendaNumber).NotEmpty();
            RuleFor(x => x.jobRotation).NotEmpty();

        }
    }
}