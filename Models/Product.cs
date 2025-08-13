using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]

        public string Name { get; set; }
        public string Description { get; set; } = "No Description";
        [Column(TypeName = "decimal(18,2)"),Required]
        
        public decimal Price { get; set; }
        [Required,Range(1,int.MaxValue)]
        public int StockQty { get; set; }
        [ForeignKey("Category"),Display(Name="Category")]
        public int CategoryId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        public Category Category { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}