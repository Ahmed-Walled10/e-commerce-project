using e_commerce_project.DTOs.Cart;
using e_commerce_project.DTOs.Wishlist;

namespace e_commerce_project.Services
{
    public interface IWishlist
    {
        Task<WishlistDTO> GetWishlist(string UserId);
        Task AddToWishlist(AddCartItemDTO item, string UserId);
        Task<CartDTO> AddToCart(AddCartItemDTO item, string UserId);
        Task RemoveFromCart(int Sku_id, string UserId);

        //Task<WishlistDTO> UpdateItemQuantity(AddCartItemDTO item, string UserId);
    }
}
