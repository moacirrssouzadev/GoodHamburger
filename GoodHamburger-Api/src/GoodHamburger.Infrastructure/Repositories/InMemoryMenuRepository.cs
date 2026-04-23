using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Infrastructure.Repositories;

/// <summary>
/// In-memory menu repository with async support.
/// Used for testing purposes.
/// </summary>
public class InMemoryMenuRepository : IMenuRepository
{
    private static readonly List<MenuItem> _menu = new()
    {
        new MenuItem(1, "X Burger", 5.00m, MenuItemType.Sandwich),
        new MenuItem(2, "X Egg",    4.50m, MenuItemType.Sandwich),
        new MenuItem(3, "X Bacon",  7.00m, MenuItemType.Sandwich),
        new MenuItem(4, "Batata frita",  2.00m, MenuItemType.Fries),
        new MenuItem(5, "Refrigerante",  2.50m, MenuItemType.Drink),
    };

    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        await Task.Delay(0); // Simulate async operation
        return _menu.AsReadOnly();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        await Task.Delay(0); // Simulate async operation
        return _menu.FirstOrDefault(m => m.Id == id);
    }
}
