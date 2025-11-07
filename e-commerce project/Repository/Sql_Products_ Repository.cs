using AutoMapper;
using e_commerce_project.DTOs;
using e_commerce_project.Modles;
using e_commerce_project.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace e_commerce_project.Repository
{
    public class Sql_Products__Repository : IProducts
    {
        private readonly sql_e_commerce_DB context;
        private readonly IMapper mapper;

        public Sql_Products__Repository(sql_e_commerce_DB _context,IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public async Task<IEnumerable<ProductsDTO>> Get_All_Products(string? name,string? description, List<int>? categoryIds, int pagenumber ,int pasgesize)
        {
            var pros = context.Products
                       .Include(x=>x.Product_Skus)
                       .Include(x=>x.Products_Categories)
                        .ThenInclude(pc=>pc.Category)
                        as IQueryable<Products>;

            if(!string.IsNullOrWhiteSpace(name))
            {
                name=name.Trim();
                pros = pros.Where(x => x.Name.ToLower() == name.ToLower());
            }
            if(!string.IsNullOrEmpty(description))
            {
                description=description.Trim();
                pros = pros.Where(x => x.Name.ToLower().Contains(description.ToLower())
                    || x.Description != null && x.Description.ToLower().Contains(description.ToLower()));
            }
            if (categoryIds != null && categoryIds.Count > 0)
            {
                pros = pros.Where(p =>
                    p.Products_Categories.Any(pc => categoryIds.Contains(pc.Category_Id)));
            }

            var pros2 = await pros.Skip((pagenumber - 1) * pasgesize)
                             .Take(pasgesize)
                             .OrderBy(x => x.Name)
                             .ToListAsync();

            return mapper.Map<IEnumerable<ProductsDTO>>(pros2);
        }
        public async Task<ProductWithSkusDTO> Get_Product_By_Id(int Id)
        {
            var pros= await context.Products
                     .Include(p=>p.Product_Skus)
                     .Include(p=>p.Products_Categories)
                     .ThenInclude(pc=>pc.Category)
                     .FirstOrDefaultAsync(x => x.Id == Id);
            
            if (pros == null)
                throw new Exception("Product not found");
            return mapper.Map<ProductWithSkusDTO>(pros);
        }
        public async Task<ProductSkuDTO> GetSkuByProductAsync(int productId, int skuId)
        {
            
            var prosku = await context.Products
                .Include(x => x.Product_Skus)
                .FirstOrDefaultAsync(p => p.Id == productId);

            var ProSku = prosku.Product_Skus.Skip(skuId-1).FirstOrDefault();

            if (ProSku == null)
                throw new Exception("SKU not found for the specified product");

            return mapper.Map<ProductSkuDTO>(ProSku);
        }
        public async Task Add_Category(CategoryDTO catdto)
        {
            var category = mapper.Map<Categories>(catdto);
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return;
        }
        public async Task Create_New_Product(CreateProductDTO prodto)
        {
            var product = mapper.Map<Products>(prodto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return;
        }
        public async Task Add_Sku_To_Product(int productId, AddProductSkuDTO skuDto)
        {
            var product = await context.Products
                .Include(p => p.Product_Skus)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new Exception("Product not found");


            var newSku = mapper.Map<Product_skus>(skuDto);
            product.Product_Skus.Add(newSku);
            await context.SaveChangesAsync();
            return;

        }
        public async Task Update_Category(int catId, JsonPatchDocument<CategoryDTO> catpatch)
        {
            var cat = await context.Categories.FirstOrDefaultAsync(c => c.Id == catId);
            if (cat == null)
                throw new Exception("Category not found");

            var catdto=mapper.Map<CategoryDTO>(cat);

            try
            {
                catpatch.ApplyTo(catdto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            mapper.Map(catdto, cat);

            await context.SaveChangesAsync();
            return;

        }
        public async Task Update_Sku(int productId, int skuId, UpdateSkuDTO UpSku)
        {
            var product = await context.Products
                .Include(p => p.Product_Skus)
                .FirstOrDefaultAsync(p => p.Id == productId);

            var sku = product.Product_Skus.First(s => s.Id == skuId);

            if (product == null)
                throw new Exception("Product not found");
            if (sku == null)
                throw new Exception("SKU not found");

            mapper.Map(UpSku, sku);
            sku.UpdatedDate = DateTime.Now;
            await context.SaveChangesAsync();


            return;
        }
        public async Task Update_Product_By_Id(int Id, UpdateProductDTO UPro)
        {
            var product = await context.Products
            .Include(p => p.Product_Skus)
            .FirstOrDefaultAsync(p => p.Id == Id);

            if (product == null)
                throw new Exception("Product not found");

            mapper.Map(UPro, product);
            product.UpdatedDate = DateTime.Now;


            /*foreach (var skuDto in UPro.Skus)
            {
                if (skuDto.Id.HasValue)
                {
                    // Update existing SKU
                    var existingSku = product.Product_Skus.FirstOrDefault(s => s.Id == skuDto.Id.Value);
                    if (existingSku != null)
                        mapper.Map(skuDto, existingSku);
                    existingSku.UpdatedDate = DateTime.Now;

                }
                else
                {
                    // Add new SKU
                    var newSku = mapper.Map<Product_skus>(skuDto);
                    product.Product_Skus.Add(newSku);
                }
            }

            // ✅ Remove SKUs not included in DTO
            var dtoSkuIds = UPro.Skus.Where(s => s.Id.HasValue).Select(s => s.Id.Value).ToList();
            var skusToRemove = product.Product_Skus.Where(s => !dtoSkuIds.Contains(s.Id)).ToList();
            context.product_Skus.RemoveRange(skusToRemove);*/

            await context.SaveChangesAsync();
            return;
        }
        public async Task Delete_Product_By_Id(int Id)
        {
            var Pro= await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (Pro == null)
                throw new Exception("Product not found");
            context.Products.Remove(Pro);
            await context.SaveChangesAsync();
            return;
        }
        public async Task Delete_Sku(int productId, int skuId)
        {
            var product = await context.Products
                .Include(p => p.Product_Skus)
                .FirstOrDefaultAsync(p => p.Id == productId);
            var sku = product.Product_Skus.FirstOrDefault(s => s.Id == skuId);

            if (product == null)
                throw new Exception("Product not found");
            if (sku == null)
                throw new Exception("SKU not found");


            context.product_Skus.Remove(sku);
            await context.SaveChangesAsync();
            return;
        }
        public async Task Delete_Category(int categoryId)
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new Exception("Category not found");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return;
        }
    }
}
