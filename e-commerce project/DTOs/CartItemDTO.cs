namespace e_commerce_project.DTOs
{
    public class CartItemDTO
    {
        public int product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;
    }
}
