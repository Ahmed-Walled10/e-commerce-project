using e_commerce_project.DTOs;
using e_commerce_project.Modles;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_project.Services
{
    
    public interface IProducts
    {
        Task<IEnumerable<ProductsDTO>> Get_All_Products(string? name, string? description, int pagenumber, int pasgesize);
        Task<ProductWithSkusDTO> Get_Product_By_Id(int Id);
        Task<ProductSkuDTO> GetSkuByProductAsync(int productId, int skuId);
        Task Create_New_Product(CreateProductDTO product);
        Task Update_Product_By_Id(int Id, UpdateProductDTO UPro);
        Task Delete_Product_By_Id(int Id);
    }
}
    