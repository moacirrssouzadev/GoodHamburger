using System.Collections.Generic;

namespace GoodHamburger.Web.Shared.Models;

public class OrderCreateDto
{
    public List<int> ItemIds { get; set; } = new();
}
