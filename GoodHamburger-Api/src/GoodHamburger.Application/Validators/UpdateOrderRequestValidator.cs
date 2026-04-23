using FluentValidation;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Validators;

/// <summary>
/// Validator for UpdateOrderRequest.
/// </summary>
public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
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
