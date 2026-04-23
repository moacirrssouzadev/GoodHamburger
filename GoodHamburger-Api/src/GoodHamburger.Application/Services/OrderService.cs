using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Helpers;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuRepository  _menuRepository;

    public OrderService(IOrderRepository orderRepository, IMenuRepository menuRepository)
    {
        _orderRepository = orderRepository;
        _menuRepository  = menuRepository;
    }

    public async Task<IEnumerable<OrderResponse>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(MapToResponse);
    }

    public async Task<OrderResponse> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Pedido '{id}' não encontrado.");

        return MapToResponse(order);
    }

    public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
    {
        var items = await ResolveAndValidateItemsAsync(request.ItemIds);

        var order = new Order();
        order.SetItems(items, DiscountCalculator.GetDiscountPercentage(items));

        var created = await _orderRepository.CreateAsync(order);
        return MapToResponse(created);
    }

    public async Task<OrderResponse> UpdateAsync(Guid id, UpdateOrderRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Pedido '{id}' não encontrado.");

        var items = await ResolveAndValidateItemsAsync(request.ItemIds);
        order.SetItems(items, DiscountCalculator.GetDiscountPercentage(items));

        var updated = await _orderRepository.UpdateAsync(order);
        return MapToResponse(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var deleted = await _orderRepository.DeleteAsync(id);
        if (!deleted)
            throw new KeyNotFoundException($"Pedido '{id}' não encontrado.");
    }

    private async Task<List<MenuItem>> ResolveAndValidateItemsAsync(List<int> itemIds)
    {
        if (itemIds is null || itemIds.Count == 0)
            throw new ArgumentException("O pedido deve conter pelo menos um item.");

        // Detect duplicate IDs in the request itself
        var duplicateIds = itemIds
            .GroupBy(id => id)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateIds.Count > 0)
            throw new ArgumentException(
                $"Itens duplicados no pedido: {string.Join(", ", duplicateIds)}.");

        // Resolve items from the menu
        var items = new List<MenuItem>();
        foreach (var id in itemIds)
        {
            var item = await _menuRepository.GetByIdAsync(id)
                ?? throw new ArgumentException($"Item com id {id} não existe no cardápio.");
            items.Add(item);
        }

        // Enforce "at most one of each type" rule
        ValidateNoDuplicateTypes(items, MenuItemType.Sandwich, "sanduíche");
        ValidateNoDuplicateTypes(items, MenuItemType.Fries,    "batata frita");
        ValidateNoDuplicateTypes(items, MenuItemType.Drink,    "refrigerante");

        // An order must have a sandwich
        if (!items.Any(i => i.Type == MenuItemType.Sandwich))
            throw new ArgumentException("O pedido deve conter pelo menos um sanduíche.");

        return items;
    }

    private static void ValidateNoDuplicateTypes(
        List<MenuItem> items, MenuItemType type, string typeName)
    {
        if (items.Count(i => i.Type == type) > 1)
            throw new ArgumentException(
                $"O pedido pode conter apenas um(a) {typeName}.");
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Mapping
    // ──────────────────────────────────────────────────────────────────────────

    private static OrderResponse MapToResponse(Order order) =>
        new(
            order.Id,
            order.Items.Select(i => new OrderItemResponse(
                i.Id, i.Name, i.Price, i.Type.ToString())).ToList(),
            order.Subtotal,
            order.DiscountPercentage,
            order.DiscountAmount,
            order.Total,
            order.CreatedAt,
            order.UpdatedAt
        );
}
