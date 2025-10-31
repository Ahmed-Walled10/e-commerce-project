namespace e_commerce_project.DTOs
{
    public class UpdateProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<UpdateSkuDTO> Skus { get; set; } = new();
    }
}
