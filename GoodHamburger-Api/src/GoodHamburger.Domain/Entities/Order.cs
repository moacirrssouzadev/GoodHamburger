using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class Order
{
    public Guid Id { get; init; }
    public List<MenuItem> Items { get; private set; } = new();
    public decimal Subtotal { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal Total { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    public Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetItems(List<MenuItem> items, decimal discountPercentage)
    {
        Items = items;
        Subtotal = items.Sum(i => i.Price);
        DiscountPercentage = discountPercentage;
        DiscountAmount = Math.Round(Subtotal * (discountPercentage / 100m), 2);
        Total = Math.Round(Subtotal - DiscountAmount, 2);
        UpdatedAt = DateTime.UtcNow;
    }

    public bool HasItemOfType(MenuItemType type) =>
        Items.Any(i => i.Type == type);

    public int CountItemsOfType(MenuItemType type) =>
        Items.Count(i => i.Type == type);
}
