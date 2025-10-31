namespace e_commerce_project.DTOs
{
    public class UpdateSkuDTO
    {
        public int? Id { get; set; } // null means new SKU
        public string Sku_Code { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
