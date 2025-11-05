using AutoMapper;
using e_commerce_project.DTOs;
using e_commerce_project.DTOs.Cart;
using e_commerce_project.Modles;
using e_commerce_project.Services;
using Microsoft.EntityFrameworkCore;
using System;
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
                  .ThenInclude(ci => ci.Sku)
                    .ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId);

            if (cart == null)
            {
                 cart = new Cart
                {
                    UserId = UserId,
                    Total = 0
                };
                await context.Carts.AddAsync(cart);
                await context.SaveChangesAsync();
            }

            return mapper.Map<CartDTO>(cart);

        }
        public async Task<CartDTO> AddToCart(AddCartItemDTO item, string UserId)
        {
            var cart = await context.Carts
                .Include(c => c.Cart_item)
                 .ThenInclude(ci => ci.Sku)
                  .ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = UserId,
                    Total = 0
                };
                await context.Carts.AddAsync(cart);
                await context.SaveChangesAsync();
            }

            var sku = await context.product_Skus
                    .Include(x=>x.Product)
                    .FirstOrDefaultAsync(s => s.Id == item.Sku_Id);

            if (sku == null)
                throw new Exception("Invalid SKU Id");


            var newitem = mapper.Map<Cart_item>(item);
            newitem.Cart_Id = cart.CartId;
            newitem.Sku = sku;

            //handle existing item
            var existingItem = cart.Cart_item
                    .FirstOrDefault(ci => ci.Sku_Id == item.Sku_Id);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Cart_item.Add(newitem);
            }

            cart.Total = cart.Cart_item.Sum(ci => ci.Subtotal);
            await context.SaveChangesAsync();

            return mapper.Map<CartDTO>(cart); 

        }
        public async Task<CartDTO> UpdateItemQuantity(AddCartItemDTO item, string UserId)
        {
            var cart = await context.Carts
                .Include(c => c.Cart_item)
                 .ThenInclude(ci => ci.Sku)
                  .ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId);

            var existingItem = cart.Cart_item
                    .FirstOrDefault(ci => ci.Sku_Id == item.Sku_Id);

               if (existingItem.Quantity + item.Quantity <= 0)
               {
                   await RemoveFromCart(existingItem.Sku_Id, UserId);
               }
               else
               {
                   existingItem.Quantity += item.Quantity;
                   cart.Total += existingItem.Subtotal;
               }

                await context.SaveChangesAsync();


                var result = mapper.Map<CartDTO>(cart);
                return result;
        }
        public async Task<CartDTO> RemoveFromCart(int Sku_id, string UserId)
        {
            var cart = await context.Carts
                .Include(c => c.Cart_item)
                 .ThenInclude(ci => ci.Sku)
                  .ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId);

            var item = cart.Cart_item.FirstOrDefault(ci => ci.Sku_Id == Sku_id);

            cart.Total -= item.Subtotal;

            context.Cart_items.Remove(item);

            await context.SaveChangesAsync();


            var result = mapper.Map<CartDTO>(cart);
            return result;
        }

    }
}
