using System.ComponentModel.DataAnnotations;

namespace FindALawyer.Dao
{
    public class LoginInput
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
