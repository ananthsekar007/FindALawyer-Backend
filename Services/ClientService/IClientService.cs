namespace FindALawyer.Services.ClientService
{
    public interface IClientService
    {
        Task<bool> IsValidClient(int clientId);
    }
}
