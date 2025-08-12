using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(25, MinimumLength = 3)]

        public string Name { get; set; }
        public string Description { get; set; } = "No Description";
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Product> Products { get; set; }
    }
}