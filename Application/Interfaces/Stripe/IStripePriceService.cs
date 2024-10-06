using Domain.Common.Stripe;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Dtos;

namespace Application.Interfaces.Stripe;

public interface IStripePriceService : IBaseCrudService<Price>
{
    Task<BaseResponse<Price>> Create(CreatePriceRequest request);
    Task<BaseResponse<object>> Delete(Guid id);
    Task<BaseResponse<IEnumerable<PriceDto>>> Get(GetPricesRequest request);
}