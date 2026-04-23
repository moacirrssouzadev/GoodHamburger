using System.Text.Json.Serialization;

namespace GoodHamburger.Web.Shared.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ItemType
{
    Sandwich,
    Fries,
    Drink
}

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemType Type { get; set; }
}
