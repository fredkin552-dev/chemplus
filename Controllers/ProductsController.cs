using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Category"] = "All Products";
        return View(ProductStore.Products);
    }

    public IActionResult Details(int id)
    {
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    public IActionResult ByCategory(string category)
    {
        var products = ProductStore.Products.Where(p => p.Category == category).ToList();
        ViewData["Category"] = category;
        return View("Index", products);
    }

    // NEW API ENDPOINT: Frontend can fetch this!
    [HttpGet("api/products")]
    public IActionResult GetProductsApi()
    {
        // This returns the data as raw JSON instead of an HTML view
        return Json(ProductStore.Products);
    }
}
