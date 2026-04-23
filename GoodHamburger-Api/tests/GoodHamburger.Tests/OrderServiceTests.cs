using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Services;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Repositories;
using Xunit;

namespace GoodHamburger.Tests;

/// <summary>
/// Uses the real in-memory repositories so we test the full application
/// behaviour without mocking internals.
/// </summary>
public class OrderServiceTests
{
    private readonly IOrderService _service;

    public OrderServiceTests()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        var menuRepo  = new InMemoryMenuRepository();
#pragma warning restore CS0618
        var orderRepo = new InMemoryOrderRepository();
        _service = new OrderService(orderRepo, menuRepo);
    }

    // ── Happy paths ───────────────────────────────────────────────────────────

    [Fact]
    public async Task CreateOrder_SandwichOnly_ShouldSucceed()
    {
        var request = new CreateOrderRequest([1]); // X Burger
        var order   = await _service.CreateAsync(request);

        Assert.NotNull(order);
        Assert.Single(order.Items);
        Assert.Equal(0m, order.DiscountPercentage);
        Assert.Equal(5.00m, order.Total);
    }

    [Fact]
    public async Task CreateOrder_FullCombo_ShouldApply20PercentDiscount()
    {
        var request = new CreateOrderRequest([1, 4, 5]); // X Burger + Fries + Drink
        var order   = await _service.CreateAsync(request);

        Assert.Equal(20m,   order.DiscountPercentage);
        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(7.60m, order.Total);
    }

    [Fact]
    public async Task GetAll_AfterCreatingTwoOrders_ShouldReturnTwo()
    {
        await _service.CreateAsync(new CreateOrderRequest([1]));
        await _service.CreateAsync(new CreateOrderRequest([2, 5]));

        var orders = await _service.GetAllAsync();
        Assert.Equal(2, orders.Count());
    }

    [Fact]
    public async Task GetById_ExistingOrder_ShouldReturnCorrectOrder()
    {
        var created   = await _service.CreateAsync(new CreateOrderRequest([3])); // X Bacon
        var retrieved = await _service.GetByIdAsync(created.Id);

        Assert.Equal(created.Id, retrieved.Id);
        Assert.Equal(7.00m, retrieved.Total);
    }

    [Fact]
    public async Task UpdateOrder_ShouldReplaceItemsAndRecalculate()
    {
        // Start with sandwich-only
        var created = await _service.CreateAsync(new CreateOrderRequest([1]));
        Assert.Equal(0m, created.DiscountPercentage);

        // Upgrade to full combo
        var updated = await _service.UpdateAsync(created.Id, new UpdateOrderRequest([1, 4, 5]));
        Assert.Equal(20m,   updated.DiscountPercentage);
        Assert.Equal(7.60m, updated.Total);
    }

    [Fact]
    public async Task DeleteOrder_ShouldRemoveFromRepository()
    {
        var created = await _service.CreateAsync(new CreateOrderRequest([1]));
        await _service.DeleteAsync(created.Id);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.GetByIdAsync(created.Id));
    }

    // ── Validation errors ─────────────────────────────────────────────────────

    [Fact]
    public async Task CreateOrder_EmptyItemList_ShouldThrowArgumentException()
    {
        await Assert.ThrowsAsync<ArgumentException>(
            () => _service.CreateAsync(new CreateOrderRequest([])));
    }

    [Fact]
    public async Task CreateOrder_DuplicateItemId_ShouldThrowArgumentException()
    {
        // Same item ID twice in the request
        var ex = await Assert.ThrowsAsync<ArgumentException>(
            () => _service.CreateAsync(new CreateOrderRequest([1, 1])));

        Assert.Contains("duplicados", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task CreateOrder_TwoSandwiches_ShouldThrowArgumentException()
    {
        // IDs 1 and 2 are both sandwiches
        var ex = await Assert.ThrowsAsync<ArgumentException>(
            () => _service.CreateAsync(new CreateOrderRequest([1, 2])));

        Assert.Contains("sanduíche", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task CreateOrder_NoSandwich_ShouldThrowArgumentException()
    {
        // IDs 4 (fries) + 5 (drink) – no sandwich
        var ex = await Assert.ThrowsAsync<ArgumentException>(
            () => _service.CreateAsync(new CreateOrderRequest([4, 5])));

        Assert.Contains("sanduíche", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task CreateOrder_InvalidMenuItemId_ShouldThrowArgumentException()
    {
        await Assert.ThrowsAsync<ArgumentException>(
            () => _service.CreateAsync(new CreateOrderRequest([99])));
    }

    [Fact]
    public async Task GetById_NonExistentOrder_ShouldThrowKeyNotFoundException()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.GetByIdAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task DeleteOrder_NonExistentOrder_ShouldThrowKeyNotFoundException()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.DeleteAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task UpdateOrder_NonExistentOrder_ShouldThrowKeyNotFoundException()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.UpdateAsync(Guid.NewGuid(), new UpdateOrderRequest([1])));
    }
}
