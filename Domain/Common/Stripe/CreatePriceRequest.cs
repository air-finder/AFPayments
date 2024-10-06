namespace Domain.Common.Stripe;

public class CreatePriceRequest(string productId, long unitAmount, string currency, string platform)
{
    public string ProductId { get; set; } = productId;
    public long UnitAmount { get; set; } = unitAmount;
    public string Currency { get; set; } = currency;
    public string Platform { get; set; } = platform;
}