using FindALawyer.Dao;

namespace FindALawyer.Services.JwtService
{
    public interface IJwtService
    {
            string CreateToken(AppUser user);
    }
}

