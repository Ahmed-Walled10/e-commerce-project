using System.ComponentModel.DataAnnotations;

namespace e_commerce_project.Modles
{
    public class Product_skus
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Sku_Code { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; } =0;
        public List<string> Pictures_Url { get; set; } = new List<string>();
        [Required]
        public string Size { get; set; } = string.Empty;
        [Required]
        public decimal weight { get; set; }
        [Required]
        public string Color { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }

        public int product_Id { get; set; }
        public virtual Products Product { get; set; }

        public virtual ICollection<Cart_item> Cart_Items { get; set; } = new List<Cart_item>();
        public virtual ICollection<WishList_products> WishList_Products { get; set; } = new List<WishList_products>();


    }
}
