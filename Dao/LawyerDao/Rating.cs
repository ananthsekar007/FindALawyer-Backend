namespace FindALawyer.Dao.LawyerDao
{
    public class Rating
    {
        public int LawyerId { get; set; }
        public int ClientId { get; set; }
        public int RatingValue { get; set; }
        
        public string Remarks { get; set; } = string.Empty;

    }
}
