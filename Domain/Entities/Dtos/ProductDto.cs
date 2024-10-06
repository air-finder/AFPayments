namespace Domain.Entities.Dtos;

public class ProductDto(Guid id, string name, string description, string platform)
{
    public Guid Id { get; set; } = id;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string Platform { get; } = platform;

    public static implicit operator ProductDto(Product product) => new ProductDto(product.Id, product.Name, product.Description, product.Platform);
}