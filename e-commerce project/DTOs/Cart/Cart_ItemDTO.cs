namespace e_commerce_project.DTOs.Cart
{
    public class Cart_ItemDTO
    {
        public string ProductName { get; set; }
        public List<string> Pictures_Url { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
