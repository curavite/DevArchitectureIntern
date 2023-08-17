
using Business.Handlers.Machines.Commands;
using FluentValidation;

namespace Business.Handlers.Machines.ValidationRules
{

    public class CreateMachineValidator : AbstractValidator<CreateMachineCommand>
    {
        public CreateMachineValidator()
        {
            RuleFor(x => x.MachineType).NotEmpty();

        }
    }
    public class UpdateMachineValidator : AbstractValidator<UpdateMachineCommand>
    {
        public UpdateMachineValidator()
        {
            RuleFor(x => x.MachineType).NotEmpty();

        }
    }
}