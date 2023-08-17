
using Business.Handlers.Errors.Commands;
using FluentValidation;

namespace Business.Handlers.Errors.ValidationRules
{

    public class CreateErrorValidator : AbstractValidator<CreateErrorCommand>
    {
        public CreateErrorValidator()
        {
            RuleFor(x => x.RowNumber).NotEmpty();
            RuleFor(x => x.Departmant).NotEmpty();
            RuleFor(x => x.ColorCode).NotEmpty();

        }
    }
    public class UpdateErrorValidator : AbstractValidator<UpdateErrorCommand>
    {
        public UpdateErrorValidator()
        {
            RuleFor(x => x.RowNumber).NotEmpty();
            RuleFor(x => x.Departmant).NotEmpty();
            RuleFor(x => x.ColorCode).NotEmpty();

        }
    }
}