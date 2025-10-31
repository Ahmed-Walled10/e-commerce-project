using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Modles
{
    public class Wishlist
    {
        public int WishlistId { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public virtual ICollection<WishList_products> WishList_Products { get; set; } = new List<WishList_products>();
    }
}
