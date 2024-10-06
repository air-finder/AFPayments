using Domain.Common.Stripe;
using Stripe;

namespace Domain.Entities;

public class Price : BaseEntity
{
    protected Price() {}

    public Price(string productId, long unitAmount, string currency, string platform, string? stripeId)
    {
        ProductId = productId;
        UnitAmount = unitAmount;
        Currency = currency;
        Platform = platform;
        StripeId = stripeId;
    }

    public string ProductId { get; } = string.Empty;
    public long UnitAmount { get; }
    public string Currency { get; } = "brl";
    public string Platform { get; } = string.Empty;
    public string? StripeId { get; private set; }
    public void SetStripeId(string stripeId) => StripeId = stripeId;
    public PriceCreateOptions ToPriceCreateOptions()
    {
        return new()
        {
            Product = ProductId,
            UnitAmount = UnitAmount,
            Currency = Currency,
            Metadata = new Dictionary<string, string>
            {
                { "platform", Platform },
                { "custom_id", Id.ToString() }
            }
        };
    }
    public static implicit operator Price(CreatePriceRequest request)
    {
        return new Price(
            request.ProductId,
            request.UnitAmount,
            request.Currency,
            request.Platform,
            null
        );
    }
}