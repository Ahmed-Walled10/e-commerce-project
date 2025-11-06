using e_commerce_project.DTOs.Cart;
using e_commerce_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace e_commerce_project.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlist context;
        public WishlistController(IWishlist _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = await context.GetWishlist(UserId);
            return Ok(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(AddCartItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             await context.AddToWishlist(item, UserId);
            return Ok();
        }

        [HttpPost("movetocart")]
        public async Task<IActionResult> MoveToCart(AddCartItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await context.AddToCart(item, UserId);
            return Ok(cart);
        }

        [HttpDelete("{Sku_id:int}")]
        public async Task<IActionResult> RemoveFromWishlist(int Sku_id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await context.RemoveFromCart(Sku_id, UserId);
            return Ok();
        }


    }
}
