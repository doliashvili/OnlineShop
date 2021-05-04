using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCommon.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Domain.CommonModels.Carts;

namespace OnlineShop.Api.Controllers
{
    [Route("api/v1/cartGuest")]
    public class CartGuestController : BaseApiController
    {
        private readonly ICartService _cartService;
        private static readonly TimeSpan ExpireTime = TimeSpan.FromMinutes(40);

        public CartGuestController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartAsync()
        {
            var data = await _cartService.GetCartAsync(RemoteIpV4).ConfigureAwait(false);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCartAsync([FromBody] CartGuest cart)//todo create dtos (avtom security ambaviao)
        {
            cart.Ip = RemoteIpV4;
            var data = await _cartService.AddCartAsync(cart, ExpireTime).ConfigureAwait(false); //todo add config expire time
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCartAsync()
        {
            var data = await _cartService.RemoveAsync(RemoteIpV4).ConfigureAwait(false);
            return Ok(data);
        }
    }
}