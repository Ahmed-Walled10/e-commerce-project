using AutoMapper;
using e_commerce_project.DTOs;
using e_commerce_project.Modles;

namespace e_commerce_project.Profiles
{
    public class CartProfile:Profile
    {
        public CartProfile()
        {
           /* CreateMap<Cart_item, CartItemDTO>()
            .ForMember(dest => dest.Product_Name,opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Product.Product_Skus.FirstOrDefault() != null
                        ? src.Product.Product_Skus.FirstOrDefault().Price
                        : 0
                ));*/
            CreateMap<Cart_item, CartItemDTO>()
          .ForMember(dest => dest.Product_Name, opt => opt.MapFrom(src => src.Product.Name))
          .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Product_Skus.FirstOrDefault().Price))
          .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        
        
        
        
        
        }
    }
}
