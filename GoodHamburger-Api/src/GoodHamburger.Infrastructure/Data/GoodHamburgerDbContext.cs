using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data;

public class GoodHamburgerDbContext : DbContext
{
    public GoodHamburgerDbContext(DbContextOptions<GoodHamburgerDbContext> options) 
        : base(options)
    {
    }

    public DbSet<MenuItem> MenuItems { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── MenuItem Configuration ──────────────────────────────────────────
        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Price).HasPrecision(10, 2).IsRequired();
            entity.Property(e => e.Type).IsRequired();

            // Seed data
            entity.HasData(
                new MenuItem(1, "X Burger", 5.00m, Domain.Enums.MenuItemType.Sandwich),
                new MenuItem(2, "X Egg", 4.50m, Domain.Enums.MenuItemType.Sandwich),
                new MenuItem(3, "X Bacon", 7.00m, Domain.Enums.MenuItemType.Sandwich),
                new MenuItem(4, "Batata frita", 2.00m, Domain.Enums.MenuItemType.Fries),
                new MenuItem(5, "Refrigerante", 2.50m, Domain.Enums.MenuItemType.Drink)
            );
        });

        // ── Order Configuration ────────────────────────────────────────────
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Subtotal).HasPrecision(10, 2).IsRequired();
            entity.Property(e => e.DiscountPercentage).HasPrecision(5, 2).IsRequired();
            entity.Property(e => e.DiscountAmount).HasPrecision(10, 2).IsRequired();
            entity.Property(e => e.Total).HasPrecision(10, 2).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();

            // Configure many-to-many relationship
            entity
                .HasMany(o => o.Items)
                .WithMany()
                .UsingEntity("OrderMenuItems",
                    l => l.HasOne(typeof(MenuItem)).WithMany().HasForeignKey("MenuItemId"),
                    r => r.HasOne(typeof(Order)).WithMany().HasForeignKey("OrderId"),
                    j => j.HasKey("OrderId", "MenuItemId"));
        });
    }
}
