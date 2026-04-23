namespace GoodHamburger.Application.DTOs;

/// <summary>
/// Response DTO for an order.
/// </summary>
public record OrderResponse(
    Guid Id,
    List<OrderItemResponse> Items,
    decimal Subtotal,
    decimal DiscountPercentage,
    decimal DiscountAmount,
    decimal Total,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
