using AutoMapper;
using e_commerce_project.DTOs.Cart;
using e_commerce_project.Modles;

namespace e_commerce_project.Profiles
{
    public class CartProfile :Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDTO>()
           .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
           .ForMember(dest => dest.Cart_Items, opt => opt.MapFrom(src => src.Cart_item));

            CreateMap<Cart_item, Cart_ItemDTO>()
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Sku.Product.Name))
           .ForMember(dest => dest.Pictures_Url, opt => opt.MapFrom(src => src.Sku.Pictures_Url))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Sku.Price))
           .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal));


            CreateMap<AddCartItemDTO, Cart_item>()
           .ForMember(dest => dest.Sku_Id, opt => opt.MapFrom(src => src.Sku_Id))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.Cart_Id, opt => opt.Ignore()) 
           .ForMember(dest => dest.Sku, opt => opt.Ignore())    
           .ForMember(dest => dest.Cart, opt => opt.Ignore());
        }
    }
}
