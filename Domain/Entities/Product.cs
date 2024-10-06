using Domain.Common.Stripe;
using Stripe;

namespace Domain.Entities;

public class Product : BaseEntity
{
    protected Product() {}
    public Product(string name, string description, List<string>? images, string platform, string? stripeId)
    {
        Name = name;
        Description = description;
        Images = images;
        Platform = platform;
        StripeId = stripeId;
    }

    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public List<string>? Images { get; }
    public string Platform { get; } = string.Empty;
    public string? StripeId { get; private set; }
    public void SetStripeId(string stripeId) => StripeId = stripeId;

    public ProductCreateOptions ToProductCreateOptions()
    {
        return new()
        {
            Name = Name,
            Description = Description,
            Images = Images,
            Metadata = new Dictionary<string, string>
            {
                { "platform", Platform },
                { "custom_id", Id.ToString() }
            }
        };
    }

    public static implicit operator Product(CreateProductRequest request)
    {
        return new Product(
            request.Name,
            request.Description,
            request.Images,
            request.Platform,
            null
        );
    }
}