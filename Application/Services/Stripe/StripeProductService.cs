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

public class StripeProductService(IProductRepository repository, IOptions<StripeSettings> stripeSettings) : BaseCrudService<Domain.Entities.Product>(repository), IStripeProductService
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;
    public async Task<BaseResponse<Domain.Entities.Product>> Create(CreateProductRequest request)
    {
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        var productService = new ProductService();
        Domain.Entities.Product product = request;
        var productOption = product.ToProductCreateOptions();
        var stripeProduct = await productService.CreateAsync(productOption);
        product.SetStripeId(stripeProduct.Id);
        await repository.InsertAsync(product);
        await repository.SaveChangesAsync();
        return new GenericResponse<Domain.Entities.Product>(product);
    }

    public async Task<BaseResponse<object>> Delete(Guid id)
    {
        var product = await repository.GetByIDAsync(id);
        if (product == null) AddNotification(NotificationMessages.NotFoundEntity(nameof(product)));
        CheckNotification();
        await repository.DeleteAsync(product);
        await repository.SaveChangesAsync();
        return new GenericResponse<object>();
    }

    public async Task<BaseResponse<IEnumerable<ProductDto>>> Get(GetProductsRequest request)
    {
        var products = await repository
            .GetAsync(x => request.Platforms == null || request.Platforms.Contains(x.Platform));
        return new GenericResponse<IEnumerable<ProductDto>>(products.Select(x => (ProductDto)x));
    }
}