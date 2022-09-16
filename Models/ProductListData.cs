using System;
namespace CartApp.Models
{
    public class ProductListData
    {

        

        public ProductListData()
        {
        }


        public static List<Product> Stock = new List<Product>()
            { new Product { Id = 1, Name = "Samsung s5", Price = 20000, Quantity = 1,ImageUrl="samsung s.jpg" },
                new Product{Id=2,Name="Iphone 4s",Price=5000.86,Quantity=1,ImageUrl="iphone4.jpg"},
                new Product{Id=3,Name="battery",Quantity=1,Price=2303.9,ImageUrl="battery.jpg" }
            };
    }
}

