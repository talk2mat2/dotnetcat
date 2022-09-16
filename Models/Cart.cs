using System;
namespace CartApp.Models
{
    public class Cart
    {
     
        IList<Product> Products { get; set; } = new List<Product>();
    }
}

