using AutoMapper;
using e_commerce_project.DTOs.Cart;
using e_commerce_project.DTOs.Wishlist;
using e_commerce_project.Modles;

namespace e_commerce_project.Profiles
{
    public class WishlistProfile:Profile
    {
        public WishlistProfile()
        {
            CreateMap<Wishlist, WishlistDTO>()
                .ForMember(dest => dest.Wishlist_Items, opt => opt.MapFrom(src => src.WishList_Products));

            

            CreateMap<WishList_products, WishlistItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Sku.Product.Name))
                .ForMember(dest => dest.Pictures_Url, opt => opt.MapFrom(src => src.Sku.Pictures_Url))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Sku.Price));


            CreateMap<AddCartItemDTO, WishList_products>()
           .ForMember(dest => dest.Sku_Id, opt => opt.MapFrom(src => src.Sku_Id))
           //.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.Wishlist_Id, opt => opt.Ignore())
           .ForMember(dest => dest.Sku, opt => opt.Ignore())
           .ForMember(dest => dest.Wishlist, opt => opt.Ignore());


        }
    }
}
