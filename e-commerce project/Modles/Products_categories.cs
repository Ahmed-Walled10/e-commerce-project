using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Modles
{
    [PrimaryKey(nameof(Product_Id), nameof(Category_Id))] 

    public class Products_categories
    {
        public int Product_Id { get; set; }
        
        public virtual Products Product { get; set; }
        public int Category_Id { get; set; }
       
        public virtual Categories Category { get; set; }
    }
}
