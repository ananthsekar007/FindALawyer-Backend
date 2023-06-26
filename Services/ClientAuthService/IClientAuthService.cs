using FindALawyer.Dao;
using FindALawyer.Dao.ClientDao;
using FindALawyer.Models;

namespace FindALawyer.Services.ClientAuthService
{
    public interface IClientAuthService
    {
        Task<ServiceResponse<ClientAuthResponse>> SignUp(ClientAuthInput authInput);

        Task<ServiceResponse<ClientAuthResponse>> Login(LoginInput loginInput);
    }
}
