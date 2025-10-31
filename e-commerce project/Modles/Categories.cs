using System.ComponentModel.DataAnnotations;

namespace e_commerce_project.Modles
{
    public class Categories
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(150)]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Products_categories> Products_Categories { get; set; } = new List<Products_categories>();
    }
}
