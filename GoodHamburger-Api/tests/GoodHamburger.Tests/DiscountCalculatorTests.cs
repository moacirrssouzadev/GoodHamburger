using GoodHamburger.Application.Helpers;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Xunit;

namespace GoodHamburger.Tests;

public class DiscountCalculatorTests
{
    // ── Fixtures ──────────────────────────────────────────────────────────────
    private static readonly MenuItem Burger = new(1, "X Burger", 5.00m, MenuItemType.Sandwich);
    private static readonly MenuItem Egg  = new(2, "X Egg", 4.50m, MenuItemType.Sandwich);
    private static readonly MenuItem Fries = new(4, "Batata frita", 2.00m, MenuItemType.Fries);
    private static readonly MenuItem Drink = new(5, "Refrigerante", 2.50m, MenuItemType.Drink);

    // ── Combo: Sandwich + Fries + Drink → 20 % ───────────────────────────────
    [Fact]
    public void FullCombo_ShouldApply20PercentDiscount()
    {
        var items = new List<MenuItem> { Burger, Fries, Drink };
        var discount = DiscountCalculator.GetDiscountPercentage(items);
        Assert.Equal(20m, discount);
    }

    // ── Combo: Sandwich + Drink → 15 % ───────────────────────────────────────
    [Fact]
    public void SandwichAndDrink_ShouldApply15PercentDiscount()
    {
        var items = new List<MenuItem> { Burger, Drink };
        var discount = DiscountCalculator.GetDiscountPercentage(items);
        Assert.Equal(15m, discount);
    }

    // ── Combo: Sandwich + Fries → 10 % ───────────────────────────────────────
    [Fact]
    public void SandwichAndFries_ShouldApply10PercentDiscount()
    {
        var items = new List<MenuItem> { Egg, Fries };
        var discount = DiscountCalculator.GetDiscountPercentage(items);
        Assert.Equal(10m, discount);
    }

    // ── Sandwich only → 0 % ──────────────────────────────────────────────────
    [Fact]
    public void SandwichOnly_ShouldApplyNoDiscount()
    {
        var items = new List<MenuItem> { Burger };
        var discount = DiscountCalculator.GetDiscountPercentage(items);
        Assert.Equal(0m, discount);
    }

    // ── Discount applies regardless of which sandwich is chosen ──────────────
    [Theory]
    [InlineData(1)]   // X Burger
    [InlineData(2)]   // X Egg
    [InlineData(3)]   // X Bacon
    public void FullCombo_AllSandwichTypes_ShouldApply20Percent(int sandwichId)
    {
        var sandwich = new MenuItem(sandwichId, "Any", 5m, MenuItemType.Sandwich);
        var items    = new List<MenuItem> { sandwich, Fries, Drink };
        var discount = DiscountCalculator.GetDiscountPercentage(items);
        Assert.Equal(20m, discount);
    }

    // ── Monetary amounts are correctly derived ────────────────────────────────
    [Fact]
    public void FullCombo_TotalShouldReflect20PercentDiscount()
    {
        // X Burger (5.00) + Batata (2.00) + Refri (2.50) = 9.50
        // 20 % of 9.50 = 1.90  → total = 7.60
        var order = new Domain.Entities.Order();
        var items = new List<MenuItem> { Burger, Fries, Drink };
        order.SetItems(items, DiscountCalculator.GetDiscountPercentage(items));

        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(20m,   order.DiscountPercentage);
        Assert.Equal(1.90m, order.DiscountAmount);
        Assert.Equal(7.60m, order.Total);
    }

    [Fact]
    public void SandwichAndDrink_TotalShouldReflect15PercentDiscount()
    {
        // X Bacon (7.00) + Refri (2.50) = 9.50  → 15 % = 1.425 ≈ 1.42 → 8.08
        var bacon = new MenuItem(3, "X Bacon", 7.00m, MenuItemType.Sandwich);
        var order = new Domain.Entities.Order();
        var items = new List<MenuItem> { bacon, Drink };
        order.SetItems(items, DiscountCalculator.GetDiscountPercentage(items));

        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(15m,   order.DiscountPercentage);
        Assert.Equal(1.42m, order.DiscountAmount);
        Assert.Equal(8.08m, order.Total);
    }

    [Fact]
    public void SandwichAndFries_TotalShouldReflect10PercentDiscount()
    {
        // X Egg (4.50) + Batata (2.00) = 6.50  → 10 % = 0.65 → 5.85
        var order = new Domain.Entities.Order();
        var items = new List<MenuItem> { Egg, Fries };
        order.SetItems(items, DiscountCalculator.GetDiscountPercentage(items));

        Assert.Equal(6.50m, order.Subtotal);
        Assert.Equal(10m,   order.DiscountPercentage);
        Assert.Equal(0.65m, order.DiscountAmount);
        Assert.Equal(5.85m, order.Total);
    }
}
