using AutoMapper;
using e_commerce_project.DTOs.Cart;
using e_commerce_project.DTOs.Wishlist;
using e_commerce_project.Modles;
using e_commerce_project.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace e_commerce_project.Repository
{
    public class Sql_Wishlist_Repository : IWishlist
    {
        private readonly sql_e_commerce_DB context;
        private readonly IMapper mapper;
        public Sql_Wishlist_Repository(sql_e_commerce_DB _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        
        
        public async Task<WishlistDTO> GetWishlist(string UserId)
        {
            var wishlist = await context.Wishlists
                  .Include(x=>x.WishList_Products)
                   .ThenInclude(wp=>wp.Sku)
                    .ThenInclude(p => p.Product)
                  .FirstOrDefaultAsync(w => w.UserId == UserId);


            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = UserId
                };
                await context.Wishlists.AddAsync(wishlist);
                await context.SaveChangesAsync();
            }

            return mapper.Map<WishlistDTO>(wishlist);

        }
        public async Task AddToWishlist(AddCartItemDTO item, string UserId)
        {
            var wishlist = await context.Wishlists
             .Include(w => w.WishList_Products)
             .FirstOrDefaultAsync(w => w.UserId == UserId);

            if (wishlist == null)
            {
                wishlist = new Wishlist { UserId = UserId };
                context.Wishlists.Add(wishlist);
                await context.SaveChangesAsync();
            }

            if (wishlist.WishList_Products.Any(wp => wp.Sku_Id == item.Sku_Id))
                return;

            
            var wishlistItem = mapper.Map<WishList_products>(item);
            wishlistItem.Wishlist_Id = wishlist.WishlistId;
            wishlist.WishList_Products.Add(wishlistItem);
            await context.SaveChangesAsync();

            return;

        }
        public async Task<CartDTO> AddToCart(AddCartItemDTO item, string UserId)
        {

            await RemoveFromCart(item.Sku_Id, UserId);
            var cartRepository = new Sql_Cart_Repository(context, mapper);
            var cart = await cartRepository.AddToCart(item, UserId);
            return mapper.Map<CartDTO>(cart);

        }
        public async Task RemoveFromCart(int Sku_id, string UserId)
        {
            var wishlist = await context.Wishlists
             .Include(w => w.WishList_Products)
             .FirstOrDefaultAsync(w => w.UserId == UserId);

            var item = wishlist.WishList_Products.FirstOrDefault(wp => wp.Sku_Id == Sku_id);

            wishlist.WishList_Products.Remove(item);

            await context.SaveChangesAsync();

            return;

        }

        /*public Task<WishlistDTO> UpdateItemQuantity(AddCartItemDTO item, string UserId)
        {
            throw new NotImplementedException();
        }*/

    }
}
