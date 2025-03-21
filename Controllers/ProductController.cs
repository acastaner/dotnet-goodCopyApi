using Microsoft.AspNetCore.Mvc;
using GoodCopyApi.Models;
using System;
using Microsoft.AspNetCore.Authentication;

namespace GoodCopyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly List<Product> Products = new List<Product>
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
    [HttpGet("{id}")] // GET /api/product/2
    public IActionResult GetProduct(int id)
    {
        Product product = GetProductById(id);
        // If product is null we create one on the fly
        product ??= new Product("RandomProduct", 29.12, 200);
        return Ok(product);
    }

    [HttpPost] // POST /api/product
    public IActionResult Post([FromBody] Product newProduct)
    {
        if (newProduct == null)
        {
            return BadRequest("Product object is null.");
        }
        int productCount = Products.Count();

        if (productCount >= 1000)
        {
            return UnprocessableEntity("Too many products exist, try deleting some.");
        }
        newProduct.Id = Products.Count() + 1;
        Products.Add(newProduct);
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }
    /// <summary>
    /// Update a product using the Id specified in the provided Product object. For the purpose of this test API, if the Product doesn't exist we'll "fake" an update.
    /// A proper Product object still needs to be provided or an error will be returned.
    /// </summary>
    /// <param name="updatedProduct">Product</param>
    /// <returns></returns>
    [HttpPut] // PUT /api/product
    public IActionResult Put([FromBody] Product updatedProduct)
    {
        if (updatedProduct == null)
        {
            return BadRequest("Product object is null.");
        }

        Product productToUpdate = GetProductById(updatedProduct.Id);
        if (productToUpdate != null)
        {
            productToUpdate.Name = updatedProduct.Name;
            productToUpdate.Price = updatedProduct.Price;
            productToUpdate.Stock = updatedProduct.Stock;
            return Ok(productToUpdate);
        }
        // If product doesn't exist, fake an update
        else
        {
            return Ok(updatedProduct);
        }
    }
    /// <summary>
    /// Deletes a Product from the specified Id. If the Id doesn't exist, still returns a success for the purpose of this test API.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")] // DEL /api/product/2
    public IActionResult Delete(int id)
    {
        Product productToDelete = GetProductById(id);
        if (productToDelete != null)
        {
            Products.Remove(productToDelete);
        }
        return NoContent();
    }

    private static Product GetProductById(int id)
    {
        return Products.FirstOrDefault(p => p.Id == id);
    }
}
