using e_commerce_project.Modles;

namespace e_commerce_project.DTOs.Cart
{
    public class CartDTO
    {
        public List<Cart_ItemDTO> Cart_Items { get; set; }
        public decimal Total { get; set; }
    }
}
