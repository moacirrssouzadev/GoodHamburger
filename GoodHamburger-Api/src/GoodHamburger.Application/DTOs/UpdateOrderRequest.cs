namespace GoodHamburger.Application.DTOs;

/// <summary>
/// Request DTO for updating an existing order.
/// </summary>
public record UpdateOrderRequest(List<int> ItemIds);
