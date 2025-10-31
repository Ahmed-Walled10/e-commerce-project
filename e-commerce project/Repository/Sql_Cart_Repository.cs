using AutoMapper;
using e_commerce_project.DTOs;
using e_commerce_project.Modles;
using e_commerce_project.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace e_commerce_project.Repository
{
    public class Sql_Cart_Repository : ICart
    {
        private readonly sql_e_commerce_DB context;
        private readonly IMapper mapper;
        public Sql_Cart_Repository(sql_e_commerce_DB _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public async Task<CartDTO> GetCart(string UserId)
        {

            var cart = await context.Carts
            .Include(c => c.Cart_item)
                .ThenInclude(ci => ci.Product)
                    .ThenInclude(p => p.Product_Skus)
            .FirstOrDefaultAsync(c => c.UserId == UserId);

            if (cart == null || cart.Cart_item == null)
            {
                return new CartDTO
                {
                    Items = new List<CartItemDTO>(),
                    Total = 0
                };
            }

            var cartItems = mapper.Map<List<CartItemDTO>>(cart.Cart_item);
            var total = cartItems.Sum(i => i.Subtotal);

            return new CartDTO
            {
                Items = cartItems,
                Total = total
            };

        }
        //Repair
        /*public async Task<CartDTO> AddToCart(AddCartItemDTO item, string UserId)
        {
             var user = await context.Users
                         .Include(u => u.Cart)
                         .ThenInclude(c => c.Cart_item)
                         .ThenInclude(ci => ci.Product)
                         .ThenInclude(p => p.Product_Skus)
                         .FirstOrDefaultAsync(u => u.Id == UserId);

            var existingItem = user.Cart.Cart_item.FirstOrDefault(ci => ci.Product_Id == item.Product_Id);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                var newItem = new Cart_item
                {
                    Product_Id = item.Product_Id,
                    Quantity = item.Quantity,
                    Cart = user.Cart
                };
                user.Cart.Cart_item.Add(newItem);
            }
            
            var cartItems = mapper.Map<List<CartItemDTO>>(user.Cart.Cart_item);
            var total = cartItems.Sum(i => i.Subtotal);
            await context.SaveChangesAsync();

            return new CartDTO
            {
                Items = cartItems,
                Total = total
            };

        }*/
        /*public async Task<CartDTO> AddItemToCartAsync(AddCartItemDTO item,string userId)
        {
            var userCart = await context.Carts
                .Include(c => c.Cart_item)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (userCart == null)
            {
                userCart = new Cart { UserId = userId };
                context.Carts.Add(userCart);
                await context.SaveChangesAsync();
            }

            var existingItem = userCart.Cart_item.FirstOrDefault(ci => ci.Product_Id == item.Product_Id);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                var product = await context.Products.FindAsync(item.Product_Id);
                if (product == null)
                    throw new Exception("Product not found");

                userCart.Cart_item.Add(new Cart_item
                {
                    Product_Id = product.Id,
                    Quantity = item.Quantity,
                });
            }

            await context.SaveChangesAsync();

            // Reload the cart with all relationships
            var updatedCart = await context.Carts
                .Include(c => c.Cart_item)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userCart.UserId);

            return mapper.Map<CartDTO>(updatedCart);
        }*/

        public async Task UpdateItemQuantity(int Proid, int quantity, string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFromCart(int Proid, string UserId)
        {
            throw new NotImplementedException();
        }

    }
}
