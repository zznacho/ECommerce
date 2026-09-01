using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public Guid CategoryId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Product() { } // Requerido por EF Core

    // Método de fábrica para asegurar la validez de la entidad al crearse
    public static Product Create(string name, string description, decimal price, int stock, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre del producto no puede estar vacío.");
        if (price <= 0)
            throw new DomainException("El precio debe ser un valor positivo.");
        if (stock < 0)
            throw new DomainException("El stock no puede ser un valor negativo.");

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Price = price,
            Stock = stock,
            CategoryId = categoryId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateDetails(string name, string description, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre del producto no puede estar vacío.");
        if (price <= 0)
            throw new DomainException("El precio debe ser un valor positivo.");
        if (stock < 0)
            throw new DomainException("El stock no puede ser un valor negativo.");

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Price = price;
        Stock = stock;
    }
}