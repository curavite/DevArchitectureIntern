
using Business.Handlers.Orders.Commands;
using FluentValidation;

namespace Business.Handlers.Orders.ValidationRules
{

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.OrderModelName).NotEmpty();
            RuleFor(x => x.OrderMaterialName).NotEmpty();

        }
    }
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderModelName).NotEmpty();
            RuleFor(x => x.OrderMaterialName).NotEmpty();

        }
    }
}