namespace GoodHamburger.Application.DTOs;

/// <summary>
/// Request DTO for creating a new order.
/// </summary>
public record CreateOrderRequest(List<int> ItemIds);
