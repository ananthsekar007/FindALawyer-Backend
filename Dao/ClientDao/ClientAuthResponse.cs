using FindALawyer.Models;

namespace FindALawyer.Dao.ClientDao
{
    public class ClientAuthResponse
    {
        public Client Client { get; set; } = new Client();

        public string AuthToken { get; set; } = string.Empty;
    }
}
