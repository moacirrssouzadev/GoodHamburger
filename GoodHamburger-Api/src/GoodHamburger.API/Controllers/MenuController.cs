using GoodHamburger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    /// <summary>
    /// Returns all available menu items.</summary>
    [HttpGet]
    public async Task<IActionResult> GetMenuAsync()
    {
        var items = await _menuService.GetMenuAsync();
        return Ok(items);
    }
}
