using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Modles
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Total_Amount { get; set; } = 0;
        public string Status { get; set; } = "Pending";
        public DateTime Order_Date { get; set; } = DateTime.Now;
        public DateTime Delivery_Date { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }

        public virtual ICollection<Order_item> Order_Item { get; set; } = new List<Order_item>();

        public int Payment_Details_Id { get; set; }
        [ForeignKey("Payment_Details_Id")]
        public virtual Payment_details Payment_Details { get; set; }
    }
}
