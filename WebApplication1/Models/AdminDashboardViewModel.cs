using System.Collections.Generic;

namespace WebApplication1.Models;

public class AdminDashboardViewModel
{
    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
    public int RecentUploadsCount { get; set; }
    public List<Product> RecentProducts { get; set; } = new List<Product>();
}