using System.ComponentModel.DataAnnotations;

namespace FindALawyer.Dao.LawyerDao
{
    public class LawyerAuthInput
    {
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [MaxLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Qualification { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
