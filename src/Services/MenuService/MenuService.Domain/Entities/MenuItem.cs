using MenuService.Domain.Enums;
using MenuService.Domain.Exceptions;

namespace MenuService.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }
    public MenuItemCategory Category { get; private set; }
    public MenuItemType Type { get; private set; }
    public bool IsAvailable { get; private set; }
    public string? ImageUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private MenuItem() { }

    public static MenuItem Create(
        string name,
        string description,
        decimal price,
        MenuItemCategory category,
        MenuItemType type,
        string? imageUrl = null)
    {
        if (price <= 0) throw new InvalidPriceException(price);

        return new MenuItem
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            Category = category,
            Type = type,
            IsAvailable = true,
            ImageUrl = imageUrl,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string description, decimal price, MenuItemCategory category, string? imageUrl)
    {
        if (price <= 0) throw new InvalidPriceException(price);

        Name = name;
        Description = description;
        Price = price;
        Category = category;
        ImageUrl = imageUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleAvailability()
    {
        IsAvailable = !IsAvailable;
        UpdatedAt = DateTime.UtcNow;
    }
}
