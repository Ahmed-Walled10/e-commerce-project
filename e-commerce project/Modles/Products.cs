using System.ComponentModel.DataAnnotations;

namespace e_commerce_project.Modles
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }

       // public virtual ICollection<WishList_products> WishList_Products { get; set; } = new List<WishList_products>();

        public virtual ICollection<Products_categories> Products_Categories { get; set; } = new List<Products_categories>();

        public virtual ICollection<Product_skus> Product_Skus { get; set; } = new List<Product_skus>();

        public virtual ICollection<Order_item> Order_Item { get; set; } = new List<Order_item>();

    }
}
