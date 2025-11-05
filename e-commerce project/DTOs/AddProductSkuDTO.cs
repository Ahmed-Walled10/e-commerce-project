using System.ComponentModel.DataAnnotations;

namespace e_commerce_project.DTOs
{
    public class AddProductSkuDTO
    {
        public string Sku_Code { get; set; }
     
        public decimal Price { get; set; }
      
        public int Quantity { get; set; }
        public List<string> Pictures_Url { get; set; } = new List<string>();
        public string Size { get; set; } 
        public decimal weight { get; set; }
        public string Color { get; set; }
    }
}
