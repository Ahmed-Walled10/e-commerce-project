using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Modles
{
    public class Cart
    {
        public int CartId { get; set; }
        public Decimal Total { get; set; } = 0;

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
       
        public virtual ICollection<Cart_item> Cart_item { get; set; } = new List<Cart_item>();
    }
}
