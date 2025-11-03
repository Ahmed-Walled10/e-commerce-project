using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Modles
{
    public class Payment_details
    {
        public int Id { get; set; }
        public Decimal Amount { get; set; } = 0;
        public string Payment_Method { get; set; } = string.Empty;
        public string Payment_Status { get; set; } = "Pending";
        public DateTime Payment_Date { get; set; } = DateTime.Now;
        public bool is_default { get; set; }

        public int Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }
    }
}
