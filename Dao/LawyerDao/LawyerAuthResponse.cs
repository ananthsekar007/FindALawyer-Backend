using FindALawyer.Models;

namespace FindALawyer.Dao.LawyerDao
{
    public class LawyerAuthResponse
    {
        public Lawyer Lawyer { get; set; } = new Lawyer();

        public string AuthToken { get; set; } = string.Empty; 
    }
}
