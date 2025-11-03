using e_commerce_project.DTOs;
using e_commerce_project.DTOs.Cart;
using e_commerce_project.Modles;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_project.Services
{
    public interface ICart
    {
        Task<CartDTO> GetCart(string UserId);
        Task<CartDTO> AddToCart(AddCartItemDTO item, string UserId);
        Task<CartDTO> UpdateItemQuantity(AddCartItemDTO item, string UserId);
        Task<CartDTO> RemoveFromCart(int Sku_id, string UserId);

    }
}
