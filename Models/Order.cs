using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public enum OrderStatus { Pending,Paid,Shipped,Completed};
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderDate { get; set; } 
        public OrderStatus Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }
        
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
    }
}
