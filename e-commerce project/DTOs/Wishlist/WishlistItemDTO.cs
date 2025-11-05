namespace e_commerce_project.DTOs.Wishlist
{
    public class WishlistItemDTO
    {
        public string ProductName { get; set; }
        public List<string> Pictures_Url { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
