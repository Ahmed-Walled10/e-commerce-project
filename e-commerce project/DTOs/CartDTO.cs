namespace e_commerce_project.DTOs
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; }
        public decimal Total { get; set; }

    }
}
