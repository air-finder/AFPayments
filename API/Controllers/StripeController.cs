using Application.Interfaces.Stripe;
using Domain.Common;
using Domain.Common.Stripe;
using Domain.Entities.Dtos;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    public class StripeController(
        IStripeProductService productService,
        IStripePriceService priceService,
        IStripePaymentService stripeService
    ) : BaseController
    {
        [HttpPost("checkout")]
        [SwaggerOperation(Summary = "Returns a link")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromBody] SinglePaymentRequest request) => Ok(await stripeService.SinglePayment(request));

        [HttpPost("product")]
        [SwaggerOperation(Summary = "Creates a product")]
        [ProducesResponseType(typeof(BaseResponse<Domain.Entities.Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            if (user!.Role != UserRole.Admin) return Unauthorized();
            return Ok(await productService.Create(request));
        }

        [HttpPost("price")]
        [SwaggerOperation(Summary = "Creates a price")]
        [ProducesResponseType(typeof(BaseResponse<Domain.Entities.Price>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreatePriceRequest request)
        {
            if (user!.Role != UserRole.Admin) return Unauthorized();
            return Ok(await priceService.Create(request));
        }

        [HttpDelete("product/{id}")]
        [SwaggerOperation(Summary = "Deletes a product")]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            if (user!.Role != UserRole.Admin) return Unauthorized();
            return Ok(await productService.Delete(id));
        }

        [HttpDelete("price/{id}")]
        [SwaggerOperation(Summary = "Deletes a price")]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePrice([FromRoute] Guid id)
        {
            if (user!.Role != UserRole.Admin) return Unauthorized();
            return Ok(await priceService.Delete(id));
        }

        [HttpGet("product")]
        [SwaggerOperation(Summary = "Returns products")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromQuery] GetProductsRequest request) => Ok(await productService.Get(request));

        [HttpGet("price")]
        [SwaggerOperation(Summary = "Returns prices")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<PriceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromQuery] GetPricesRequest request) => Ok(await priceService.Get(request));
    }
}
