using AutoMapper;
using e_commerce_project.DTOs;
using e_commerce_project.DTOs.Cart;
using e_commerce_project.Modles;
using e_commerce_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace e_commerce_project.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICart context;
        public CartController(ICart _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await context.GetCart(UserId);
            return Ok(cart);
        }
            
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddCartItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var result = await context.AddToCart(item, UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateItemQuantity(AddCartItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await context.UpdateItemQuantity(item,UserId);
            return Ok(cart);
        }

        [HttpDelete("{Sku_id:int}")]
        public async Task<IActionResult> RemoveFromCart(int Sku_id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await context.RemoveFromCart(Sku_id, UserId);
            return Ok(cart);

        }


    }

}
