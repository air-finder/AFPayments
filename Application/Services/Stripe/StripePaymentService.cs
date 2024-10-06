using Application.Interfaces.Stripe;
using Domain.Common.Stripe;
using Domain.Common;
using Infra.Utils.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Application.Services.Stripe;

public class StripePaymentService(IOptions<StripeSettings> stripeSettings) : IStripePaymentService
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;
    public async Task<BaseResponse<string>> SinglePayment(SinglePaymentRequest request)
    {
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = request.PaymentMethods,
            LineItems = request.Prices.Select(x => x.ToSessionLineItemOptions()).ToList(),
            Mode = "payment",
            SuccessUrl = request.SuccessUrl,
            CancelUrl = request.CancelUrl,
            AllowPromotionCodes = request.AllowPromotionCode
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return new GenericResponse<string>(session.Url);
    }
}