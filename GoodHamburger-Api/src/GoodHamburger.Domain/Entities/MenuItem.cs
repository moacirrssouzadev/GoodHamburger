using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class MenuItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public MenuItemType Type { get; init; }

    public MenuItem() { }

    public MenuItem(int id, string name, decimal price, MenuItemType type)
    {
        Id = id;
        Name = name;
        Price = price;
        Type = type;
    }
}
