namespace e_commerce_project.DTOs.Cart
{
    public class AddCartItemDTO
    {
        public int Sku_Id { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
