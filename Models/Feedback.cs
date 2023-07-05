using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindALawyer.Models
{
    [Table("feedback")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        [ForeignKey("Lawyer")]
        public int LawyerId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public int Rating { get; set; }

        public string Remarks { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual Lawyer Lawyer { get; set; }
        public virtual Client Client { get; set; }
    }
}
