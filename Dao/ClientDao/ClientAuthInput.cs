using System.ComponentModel.DataAnnotations;

namespace FindALawyer.Dao.ClientDao
{
    public class ClientAuthInput
    {
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [MaxLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
