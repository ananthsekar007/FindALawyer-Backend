using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FindALawyer.Models
{
    [Table("payments")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }

        public float Amount { get; set; }

        public string Status { get; set; } = "PENDING";

        [AllowNull]
        [ForeignKey("RazorPayments")]
        public int? payment_reference_id { get; set; } = null;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual Appointment Appointment { get; set; }
        public virtual RazorPayPayments RazorPayments { get; set;}
    }
}
