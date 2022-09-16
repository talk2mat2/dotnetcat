using System;
namespace CartApp.Models
{
    public class Product
    {
        //public Product()
        //{

        //}
        //Name, Unique number, Price and Quantity.
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = "";


    }
}

