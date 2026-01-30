using FluentValidation;
using OrderSystem.Application.Orders.Commands.CreateOrder;

namespace OrderSystem.Application.Validator;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(o => o.Products)
            .NotNull().WithMessage("product list can´t be null")
            .NotEmpty().WithMessage("product list can´t be empty");

        RuleForEach(o => o.Products)
            .Must(p => p.Quantity > 0).WithMessage("Product quantity must be bigger then zero");

        RuleForEach(o => o.Products)
            .Must(p => p.ProductId != Guid.Empty).WithMessage("ProductId can´t be empty");
    }
}
