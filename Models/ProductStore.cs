using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models;

public static class ProductStore
{
    public static List<Product> Products { get; } = new List<Product>
    {
        new Product 
        { 
            Id = 1, 
            Name = "UltraClean Laundry Detergent", 
            Description = "A concentrated laundry detergent that delivers powerful cleaning and bright results on all fabrics.",
            Price = 14.99m, 
            Category = "Laundry",
            ImageUrl = "https://via.placeholder.com/300?text=Laundry+Detergent",
            Stock = 80
        },
        new Product 
        { 
            Id = 2, 
            Name = "Sparkle Dishwashing Liquid", 
            Description = "Grease-fighting dish soap that cleans dishes quickly while leaving a fresh scent.",
            Price = 5.99m, 
            Category = "Cleaning",
            ImageUrl = "https://via.placeholder.com/300?text=Dishwashing+Liquid",
            Stock = 120
        },
        new Product 
        { 
            Id = 3, 
            Name = "Fresh Breeze Fabric Softener", 
            Description = "Softens clothes and adds a long-lasting fresh fragrance to every wash.",
            Price = 7.49m, 
            Category = "Laundry",
            ImageUrl = "https://via.placeholder.com/300?text=Fabric+Softener",
            Stock = 70
        },
        new Product 
        { 
            Id = 4, 
            Name = "All-Purpose Surface Cleaner", 
            Description = "A versatile cleaning spray for countertops, glass, and hard surfaces with streak-free shine.",
            Price = 9.99m, 
            Category = "Cleaning",
            ImageUrl = "https://via.placeholder.com/300?text=Surface+Cleaner",
            Stock = 90
        },
        new Product 
        { 
            Id = 5, 
            Name = "PowerClean Powder Detergent", 
            Description = "A heavy-duty detergent powder designed for tough stains and everyday laundry care.",
            Price = 18.99m, 
            Category = "Detergent",
            ImageUrl = "https://via.placeholder.com/300?text=Powder+Detergent",
            Stock = 60
        },
        new Product 
        { 
            Id = 6, 
            Name = "Pristine Glass Cleaner", 
            Description = "A fast-acting glass cleaner that removes fingerprints and dust for a crystal-clear finish.",
            Price = 6.99m, 
            Category = "Cleaning",
            ImageUrl = "https://via.placeholder.com/300?text=Glass+Cleaner",
            Stock = 50
        }
    };
}
