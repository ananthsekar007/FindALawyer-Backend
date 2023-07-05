using FindALawyer.Data;
using FindALawyer.Models;

namespace FindALawyer.Services.ClientService
{
    public class ClientServiceImpl : IClientService
    {
        private readonly FindALawyerContext _context;
        public ClientServiceImpl(FindALawyerContext context) { 
            _context = context;
        }

        public async Task<bool> IsValidClient(int clientId)
        {
            Client existingClient = await _context.Client.FindAsync(clientId);
            if (existingClient == null) return false;
            return true;
        }
    }
}
