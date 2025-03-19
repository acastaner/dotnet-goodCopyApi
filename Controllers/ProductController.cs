using Microsoft.AspNetCore.Mvc;
using GoodCopyApi.Models;
using System;

namespace GoodCopyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly Product[] Products = new Product[]
    {
        new Product { Id = 1, Name = "Product1", Price = 10.99, Stock = 50 },
            new Product { Id = 2, Name = "Product2", Price = 20.99, Stock = 30 },
            new Product { Id = 3, Name = "Product3", Price = 30.99, Stock = 20 },
            new Product { Id = 4, Name = "Product4", Price = 40.99, Stock = 10 },
            new Product { Id = 5, Name = "Product5", Price = 50.99, Stock = 5 },
            new Product { Id = 6, Name = "Product6", Price = 60.99, Stock = 2 },
            new Product { Id = 7, Name = "Product7", Price = 70.99, Stock = 1 },
            new Product { Id = 8, Name = "Product8", Price = 80.99, Stock = 0 },
            new Product { Id = 9, Name = "Product9", Price = 90.99, Stock = 0 },
            new Product { Id = 10, Name = "Product10", Price = 100.99, Stock = 0 }
    };

    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet] // GET /api/product
    public IEnumerable<Product> Get()
    {
        return Products;
    }
    [HttpGet("{id}")] // GET /api/2
    public Product Get(int id)
    {
        Product product = GetProductById(id);
        // If product is null we create one on the fly
        product ??= new Product("RandomProduct", 29.12, 200);
        return product;
    }

    private static Product GetProductById(int id)
    {
        return Products.FirstOrDefault(p => p.Id == id);
    }
}
