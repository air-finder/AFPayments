namespace Domain.Common.Stripe;

public class GetPricesRequest
{
    public List<string>? Platforms { get; set; }
}