using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
        new Product { Id = 2, Name = "Smartphone", Price = 699.99m, Category = "Electronics" },
        new Product { Id = 3, Name = "Headphones", Price = 149.99m, Category = "Accessories" },
        new Product { Id = 4, Name = "Coffee Maker", Price = 89.99m, Category = "Home Appliances" },
        new Product { Id = 5, Name = "Fitness Tracker", Price = 129.99m, Category = "Wearables" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}
