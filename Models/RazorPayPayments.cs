using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FindALawyer.Models
{
    [Table("razorpay_payments")]
    public class RazorPayPayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RazorPayId { get; set; }

        public string PaymenrOrderId { get; set; } = string.Empty;

        [AllowNull]
        public string? PaymentId { get; set; } = string.Empty;

        public float Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
