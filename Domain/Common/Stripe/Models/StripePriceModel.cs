using Stripe.Checkout;

namespace Domain.Common.Stripe.Models;

public class StripePriceModel
{
    public string StripeId { get; set; }
    public int Quantity { get; set; }

    public SessionLineItemOptions ToSessionLineItemOptions()
    {
        return new()
        {
            Price = StripeId,
            Quantity = Quantity
        };
    }
}