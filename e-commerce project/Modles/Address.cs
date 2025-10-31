using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Modles
{
    public class Address
    {
        public int AddressId { get; set; }
        [Required]
        [StringLength(100)]
        public string street_line1 { get; set; } = string.Empty;
        [StringLength(100)]
        public string? street_line2 { get; set; }
        [Required]
        public string city { get; set; } = string.Empty;
        [Required]
        public string state { get; set; } = string.Empty;
        [DataType(DataType.PostalCode)]
        [Required]
        public int Postal_code { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Phone_number { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int? Phone_number2 { get; set; }
        public bool is_deafult { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }


    }

}
