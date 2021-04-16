using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiCommon.BaseControllers;
using OnlineShop.Domain.Products.Commands;
using OnlineShop.Domain.Products.Queries;
using OnlineShop.Domain.Products.ReadModels;

// **************************************************
//                                                 //
//  Code generated by Levan Doliashvili "API Generator"  //
//												   //
// **************************************************

namespace OnlineShop.Api.Controllers
{
    [ApiController]
	[Route("v1/Product")]
	public class ProductController : BaseApiController
	{
		[HttpPost("CreateProduct")]
		public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

		[HttpPost("AddProductImage")]
		public async Task<IActionResult> AddProductImageAsync([FromBody] AddProductImageCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

		[HttpPost("DeleteProductImage")]
		public async Task<IActionResult> DeleteProductImageAsync([FromBody] DeleteProductImageCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProductAsync(
            [FromBody] DeleteProductCommand command)
        {
            await Mediator.SendAsync(command).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("ChangeProductName")]
		public async Task<IActionResult> ChangeProductNameAsync([FromBody] ChangeProductNameCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}	

		[HttpPost("ChangeProductPrice")]
		public async Task<IActionResult> ChangeProductPriceAsync([FromBody] ChangeProductPriceCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}	

		[HttpPost("ChangeProductBrand")]
		public async Task<IActionResult> ChangeProductBrandAsync([FromBody] ChangeProductBrandCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

		[HttpPost("ChangeProductColor")]
		public async Task<IActionResult> ChangeProductColorAsync([FromBody] ChangeProductColorCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

		[HttpPost("ChangeProductType")]
		public async Task<IActionResult> ChangeProductTypeAsync([FromBody] ChangeProductTypeCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

		[HttpPost("ChangeProductDiscount")]
		public async Task<IActionResult> ChangeProductDiscountAsync([FromBody] ChangeProductDiscountCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

		[HttpPost("ChangeProductForBaby")]
		public async Task<IActionResult> ChangeProductForBabyAsync([FromBody] ChangeProductForBabyCommand command) 
		{
			await Mediator.SendAsync(command).ConfigureAwait(false);
			return Ok();
		}

		[HttpGet("GetAllProducts")]
		[ProducesResponseType(typeof(List<ProductReadModel>), 200)]
		public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProducts query) 
		{
			var data = await Mediator.QueryAsync(query).ConfigureAwait(false);
			return Ok(data);
		}

		[HttpGet("GetAllProductCount")]
		[ProducesResponseType(typeof(int), 200)]
		public async Task<IActionResult> GetAllProductCountAsync([FromQuery] GetAllProductCount query) 
		{
			var data = await Mediator.QueryAsync(query).ConfigureAwait(false);
			return Ok(data);
		}

		[HttpGet("GetProducts")]
		[ProducesResponseType(typeof(PagingProductModel), 200)]
		public async Task<IActionResult> GetProductsAsync([FromQuery] GetProducts query) 
		{
			var data = await Mediator.QueryAsync(query).ConfigureAwait(false);
			return Ok(data);
		}

		[HttpGet("GetFilteredProducts")]
		[ProducesResponseType(typeof(PagingProductModel), 200)]
		public async Task<IActionResult> GetFilteredProductsAsync([FromQuery] GetFilteredProducts query) 
		{
			var data = await Mediator.QueryAsync(query).ConfigureAwait(false);
			return Ok(data);
		}

		[HttpGet("GetProductById")]
		[ProducesResponseType(typeof(ProductReadModel), 200)]
		public async Task<IActionResult> GetProductByIdAsync([FromQuery] GetProductById query) 
		{
			var data = await Mediator.QueryAsync(query).ConfigureAwait(false);
			return Ok(data);
		}
	}
}