using Domain.Common.Stripe;
using Domain.Common;

namespace Application.Interfaces.Stripe;

public interface IStripePaymentService
{
    Task<BaseResponse<string>> SinglePayment(SinglePaymentRequest request);
}