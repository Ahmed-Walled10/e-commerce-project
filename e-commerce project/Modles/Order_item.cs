using System.ComponentModel.DataAnnotations;

namespace e_commerce_project.Modles
{
    public class Order_item
    {
        public int Order_itemId { get; set; }
        public int Quantity { get; set; } = 1;

        public int product_Id { get; set; }
        public virtual Products Product { get; set; }

        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }
    }
}
