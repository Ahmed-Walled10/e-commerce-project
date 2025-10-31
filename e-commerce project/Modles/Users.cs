using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Modles
{
    public class Users : IdentityUser
    {
       // public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string First_Name { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Last_Name { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; }

        /*[Required]
       [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;*/
       /* [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }*/

        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthdate { get; set; }

        public DateTime Created_at { get; set; }= DateTime.Now;


        public virtual ICollection<Address>? Addresses { get; set; }= new List<Address>();

        public int wishlist_Id { get; set; }
        [ForeignKey("wishlist_Id")]
        public virtual Wishlist? Wishlist { get; set; } 

        //public int Cart_Id { get; set; }
        //[ForeignKey("Cart_Id")]
        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Order> Orders { get; set; }= new List<Order>();

    }
}
