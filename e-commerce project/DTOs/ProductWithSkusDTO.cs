namespace e_commerce_project.DTOs
{
    public class ProductWithSkusDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductSkuDTO> Skus { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
