using FluentValidation;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Validators;

/// <summary>
/// Validator for CreateOrderRequest.
/// </summary>
public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.ItemIds)
            .NotNull()
            .WithMessage("ItemIds is required");

        RuleFor(x => x.ItemIds)
            .NotEmpty()
            .WithMessage("At least one item must be selected");

        RuleFor(x => x.ItemIds)
            .Must(items => items.All(id => id > 0))
            .WithMessage("All item IDs must be greater than zero");

        RuleFor(x => x.ItemIds)
            .Must(items => items.Count <= 20)
            .WithMessage("Maximum of 20 items per order");
    }
}
