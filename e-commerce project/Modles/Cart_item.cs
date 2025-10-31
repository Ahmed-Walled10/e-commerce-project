using System.ComponentModel.DataAnnotations;

namespace e_commerce_project.Modles
{
    public class Cart_item
    {
        public int Cart_itemId { get; set; }
        public int Quantity { get; set; } = 1;

        public int Cart_Id { get; set; }
        public Cart Cart { get; set; }

        public int Product_Id { get; set; }
        public virtual Products Product { get; set; }
    }
}
