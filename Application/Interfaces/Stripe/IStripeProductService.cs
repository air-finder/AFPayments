using Domain.Common.Stripe;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Dtos;

namespace Application.Interfaces.Stripe;

public interface IStripeProductService : IBaseCrudService<Product>
{
    Task<BaseResponse<Product>> Create(CreateProductRequest request);
    Task<BaseResponse<object>> Delete(Guid id);
    Task<BaseResponse<IEnumerable<ProductDto>>> Get(GetProductsRequest request);
}