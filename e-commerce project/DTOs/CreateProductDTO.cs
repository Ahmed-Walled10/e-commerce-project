namespace e_commerce_project.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CreateProductSkuDTO> Skus { get; set; }
        public List<CreateProductCategoryDTO> Categories { get; set; }
    }
}
