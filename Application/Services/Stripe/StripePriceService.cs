using Application.Interfaces.Stripe;
using Domain.Common;
using Domain.Common.Stripe;
using Domain.Constants;
using Domain.Entities.Dtos;
using Domain.Repositories;
using Infra.Utils.Configuration;
using Microsoft.Extensions.Options;
using Stripe;

namespace Application.Services.Stripe;

public class StripePriceService(
    IPriceRepository repository,
    IOptions<StripeSettings> stripeSettings
) : BaseCrudService<Domain.Entities.Price>(repository), IStripePriceService
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;
    public async Task<BaseResponse<Domain.Entities.Price>> Create(CreatePriceRequest request)
    {
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        var priceService = new PriceService();
        Domain.Entities.Price price = request;
        var priceOptions = price.ToPriceCreateOptions();
        var stripePrice = await priceService.CreateAsync(priceOptions);
        price.SetStripeId(stripePrice.Id);
        await repository.InsertAsync(price);
        await repository.SaveChangesAsync();
        return new GenericResponse<Domain.Entities.Price>(price);
    }

    public async Task<BaseResponse<object>> Delete(Guid id)
    {
        var price = await repository.GetByIDAsync(id);
        if (price == null) AddNotification(NotificationMessages.NotFoundEntity(nameof(price)));
        CheckNotification();
        await repository.DeleteAsync(price!);
        await repository.SaveChangesAsync();
        return new GenericResponse<object>();
    }

    public async Task<BaseResponse<IEnumerable<PriceDto>>> Get(GetPricesRequest request)
    {
        var prices = await repository
            .GetAsync(x => request.Platforms == null || request.Platforms.Contains(x.Platform));
        return new GenericResponse<IEnumerable<PriceDto>>(prices.Select(x => (PriceDto)x));
    }
}