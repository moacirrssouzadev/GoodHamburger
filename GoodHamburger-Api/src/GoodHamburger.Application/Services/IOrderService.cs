using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponse>> GetAllAsync();
    Task<OrderResponse> GetByIdAsync(Guid id);
    Task<OrderResponse> CreateAsync(CreateOrderRequest request);
    Task<OrderResponse> UpdateAsync(Guid id, UpdateOrderRequest request);
    Task DeleteAsync(Guid id);
}
