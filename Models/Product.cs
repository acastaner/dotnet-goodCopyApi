namespace GoodCopyApi.Models;
using System.ComponentModel.DataAnnotations;
using System;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int Stock { get; set; }

    public Product()
    { }

    public Product(int id, string name, double price, int stock)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
        Stock = stock;
    }

    public Product(string name, double price, int stock)
    {
        Random random = new Random();
        Id = random.Next(11, 100);
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
        Stock = stock;
    }
}
