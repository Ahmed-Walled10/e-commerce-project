using AutoMapper;
using e_commerce_project.DTOs;
using e_commerce_project.Modles;

namespace e_commerce_project.Services
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Products, ProductsDTO>()
            .ForMember(dest => dest.Lowest_Price,
            opt => opt.MapFrom(src =>
            src.Product_Skus != null 
         && src.Product_Skus.Any()?
            src.Product_Skus.Min(s => s.Price):0));

            CreateMap<Product_skus, ProductSkuDTO>();
            CreateMap<Categories, CategoryDTO>();
            CreateMap<Products, ProductWithSkusDTO>()
            .ForMember(dest => dest.Skus, opt => opt.MapFrom(src => src.Product_Skus))
            .ForMember(dest => dest.Categories,opt => opt.MapFrom(src => src.Products_Categories.Select(pc=>pc.Category)));

            CreateMap<Products, UpdateProductDTO>().ReverseMap();
            CreateMap<Product_skus, UpdateSkuDTO>().ReverseMap();

            CreateMap<Product_skus, ProductSkuDTO>().ReverseMap();

            CreateMap<CreateProductDTO, Products>()
                    .ForMember(dest => dest.Product_Skus, opt => opt.MapFrom(src => src.Skus))
                    .ForMember(dest => dest.Products_Categories, opt => opt.MapFrom(src =>
                    src.Categories.Select(c => new Products_categories
                    {
                        Category = new Categories
                        {
                            Name = c.Name,
                            Description = c.Description
                        }
                    })));
            CreateMap<CreateProductSkuDTO, Product_skus>();
            CreateMap<CreateProductCategoryDTO, Categories>();




        }
    }
}
