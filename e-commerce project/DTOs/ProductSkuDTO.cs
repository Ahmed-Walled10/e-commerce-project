namespace e_commerce_project.DTOs
{
    public class ProductSkuDTO
    {
        public string Sku_Code { get; set; } = null!;
        public decimal Price { get; set; }
        public List<string> Pictures_Url { get; set; }
        public string Size { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
    }
}
