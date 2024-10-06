using Domain.Common.Stripe.Models;

namespace Domain.Common.Stripe;

public class SinglePaymentRequest
{
    public List<string> PaymentMethods { get; set; } = ["card", "boleto"];
    public string CancelUrl { get; set; }
    public string SuccessUrl { get; set; }
    public List<StripePriceModel> Prices { get; set; }
    public bool AllowPromotionCode { get; set; }
}