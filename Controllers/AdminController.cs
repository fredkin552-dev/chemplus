using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Controllers;

public class AdminController : Controller
{
    // Dashboard
    public IActionResult Index()
    {
        var products = ProductStore.Products;
        var viewModel = new AdminDashboardViewModel
        {
            TotalProducts = products.Count,
            TotalCategories = products.Select(p => p.Category).Distinct().Count(),
            RecentUploadsCount = products.Count > 3 ? 3 : products.Count,
            RecentProducts = products.OrderByDescending(p => p.Id).Take(5).ToList()
        };

        return View(viewModel);
    }

    // Product Management - List
    public IActionResult Products()
    {
        return View(ProductStore.Products);
    }

    // Product Management - Add (GET)
    public IActionResult Create()
    {
        return View(new Product());
    }

    // Product Management - Add (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            product.Id = ProductStore.Products.Any() ? ProductStore.Products.Max(p => p.Id) + 1 : 1;
            
            if (string.IsNullOrEmpty(product.ImageUrl))
            {
                product.ImageUrl = "https://via.placeholder.com/300?text=New+Product";
            }

            ProductStore.Products.Add(product);
            return RedirectToAction(nameof(Products));
        }
        return View(product);
    }

    // Product Management - Edit (GET)
    public IActionResult Edit(int id)
    {
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    // Product Management - Edit (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Product updatedProduct)
    {
        if (id != updatedProduct.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var existingProduct = ProductStore.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.ImageUrl = updatedProduct.ImageUrl;
                existingProduct.Stock = updatedProduct.Stock;
            }
            return RedirectToAction(nameof(Products));
        }
        return View(updatedProduct);
    }

    // Product Management - Delete (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            ProductStore.Products.Remove(product);
        }
        return RedirectToAction(nameof(Products));
    }
}