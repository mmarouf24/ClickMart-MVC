using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        //price during adding
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        
    }
}
