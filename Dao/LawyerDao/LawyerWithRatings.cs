using FindALawyer.Models;

namespace FindALawyer.Dao.LawyerDao
{
    public class LawyerWithRatings
    {
        public int LawyerId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Qualification { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public float AverageRating { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
