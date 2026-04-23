using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Services;

public interface IMenuService
{
    Task<IEnumerable<MenuItemResponse>> GetMenuAsync();
}

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<IEnumerable<MenuItemResponse>> GetMenuAsync()
    {
        var items = await _menuRepository.GetAllAsync();
        return items.Select(item => new MenuItemResponse(
            item.Id,
            item.Name,
            item.Price,
            item.Type.ToString()
        ));
    }
}
