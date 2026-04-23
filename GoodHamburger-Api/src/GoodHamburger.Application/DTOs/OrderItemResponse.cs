namespace GoodHamburger.Application.DTOs;

/// <summary>
/// Response DTO for an order item.
/// </summary>
public record OrderItemResponse(
    int Id,
    string Name,
    decimal Price,
    string Type
);
