using FindALawyer.Dao.ClientDao;
using FindALawyer.Dao;
using FindALawyer.Dao.LawyerDao;

namespace FindALawyer.Services.LawyerAuthService
{
    public interface ILawyerAuthService
    {
        Task<ServiceResponse<LawyerAuthResponse>> SignUp(LawyerAuthInput authInput);

        Task<ServiceResponse<LawyerAuthResponse>> Login(LoginInput loginInput);
    }
}
