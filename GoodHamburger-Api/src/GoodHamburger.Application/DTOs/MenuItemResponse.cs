namespace GoodHamburger.Application.DTOs;

/// <summary>
/// Response DTO for a menu item.
/// </summary>
public record MenuItemResponse(
    int Id,
    string Name,
    decimal Price,
    string Type
);
