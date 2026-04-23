using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Helpers;

/// <summary>
/// Applies the "Good Hamburger" combo discount rules:
///   Sandwich + Fries + Drink  → 20 %
///   Sandwich + Drink          → 15 %
///   Sandwich + Fries          → 10 %
///   Sandwich only             →  0 %
/// </summary>
public static class DiscountCalculator
{
    public static decimal GetDiscountPercentage(IEnumerable<MenuItem> items)
    {
        var list = items.ToList();

        bool hasSandwich = list.Any(i => i.Type == MenuItemType.Sandwich);
        bool hasFries    = list.Any(i => i.Type == MenuItemType.Fries);
        bool hasDrink    = list.Any(i => i.Type == MenuItemType.Drink);

        return (hasSandwich, hasFries, hasDrink) switch
        {
            (true, true, true)   => 20m,
            (true, false, true)  => 15m,
            (true, true, false)  => 10m,
            _                    => 0m
        };
    }
}
