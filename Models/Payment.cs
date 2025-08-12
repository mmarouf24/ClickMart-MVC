using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        BankTransfer,
        Cash
    }
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DateTime PaymentDate { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        [MaxLength(100)]

        public string TransactionId { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}