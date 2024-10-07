namespace Domain.Common.Stripe;

public class CreateProductRequest(string name, string description, List<string>? images, string platform)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public List<string>? Images { get; set; } = images;
    public string Platform { get; set; } = platform;
}