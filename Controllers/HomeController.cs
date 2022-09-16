using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CartApp.Models;
using System.Linq;
using CartApp.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using Microsoft.VisualBasic;
using System.Text;
using static iTextSharp.text.pdf.AcroFields;

namespace CartApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
       
        ViewBag.Products = ProductListData.Stock;
        var Cart = HttpContext.Session.Get<List<Product>>("Cart");
        ViewData["Cart"] = Cart;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Cart() { 
       
        var Cart = HttpContext.Session.Get<List<Product>>("Cart");
        var Discount = HttpContext.Session.Get<double>("Discount");
        ViewData["Cart"] = Cart;
        ViewData["Discount"] = Discount;
         return View();
    }

    [HttpPost]
    public IActionResult AddCart(int id)
    {
        Console.WriteLine(id);
        var ItemToAdd = ProductListData.Stock.Find(p => p.Id == id);
        if (ItemToAdd != null)
        {
            var CartItem = HttpContext.Session.Get<List<Product>>("Cart");
            if (CartItem!= null)
            {
                var index = CartItem.FindIndex(p => p.Id == ItemToAdd.Id);
                if (index != -1)
                {
                    CartItem[index].Quantity += ItemToAdd.Quantity;
                    HttpContext.Session.Set<List<Product>>("Cart", CartItem);
                }
                else
                {
                    CartItem.Add(ItemToAdd);
                    HttpContext.Session.Set<List<Product>>("Cart", CartItem);
                }
             
            }
            else
            {
                var Cart = new List<Product>();
                Cart.Add(ItemToAdd);
                HttpContext.Session.Set<List<Product>>("Cart", Cart);
            }
            
            TempData["message"] = "The item has been added to cart";
        }
        else
        {
            
            TempData["message"] = "the item is not available";
        }
        
        return Redirect("/");
    }

    [HttpPost]
    public IActionResult Discount(double Discount)
    {
        if (Discount<0)
        {
            return Redirect("Cart");
        }
        else
        {
            Console.WriteLine(Discount);
            HttpContext.Session.Set<double>("Discount", Discount);
            return RedirectToAction("Cart");
        }
  
    }

    public IActionResult ClearCart()
    {

        HttpContext.Session.Clear();

        return RedirectToAction("Cart");

    }

    public IActionResult CheckOut(double CheckoutTotal)
    {

        ViewData["CheckoutTotal"] = CheckoutTotal;
        var Cart = HttpContext.Session.Get<List<Product>>("Cart");
        var Discount = HttpContext.Session.Get<double>("Discount");
        ViewData["Cart"] = Cart;
        ViewData["Discount"] = Discount;
        return View();
    }

    [HttpPost]
    public FileResult Export(double CheckoutTotal)
    {

        var Cart = HttpContext.Session.Get<List<Product>>("Cart");
        string userData = "";
       
        foreach (Product item in Cart!)
                {
            string Single ="product= "+  item.Name + "," + "Quantity= " + item.Quantity + Environment.NewLine;
            userData += Single;
                }
        
        var byteArray = Encoding.ASCII.GetBytes(userData);
       var stream = new MemoryStream(byteArray);
        return File(stream, "text/plain", "your_file_name.txt");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

