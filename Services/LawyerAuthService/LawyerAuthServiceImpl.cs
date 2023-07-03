using FindALawyer.Dao;
using FindALawyer.Dao.ClientDao;
using FindALawyer.Dao.LawyerDao;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Services.JwtService;
using FindALawyer.Services.PasswordService;
using Microsoft.EntityFrameworkCore;

namespace FindALawyer.Services.LawyerAuthService
{
    public class LawyerAuthServiceImpl : ILawyerAuthService
    {

        private readonly FindALawyerContext _context;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public LawyerAuthServiceImpl(FindALawyerContext context, IPasswordService passwordService, IJwtService jwtService)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse<LawyerAuthResponse>> Login(LoginInput loginInput)
        {
            ServiceResponse<LawyerAuthResponse> serviceResponse = new ServiceResponse<LawyerAuthResponse>();

            if (_context.Lawyer is null)
            {
                serviceResponse.Error = "Lawyer Context not found";
                return serviceResponse;
            }

            Lawyer existingLawyer = await _context.Lawyer.FirstOrDefaultAsync(c => c.EmailAddress == loginInput.Email);

            if (existingLawyer is null)
            {
                serviceResponse.Error = "No user exists with this email!";
                return serviceResponse;
            }

            if (!_passwordService.VerifyPasswordHash(loginInput.Password, existingLawyer.PasswordHash, existingLawyer.PasswordSalt))
            {
                serviceResponse.Error = "Please check the credentials and try again!";
                return serviceResponse;
            }

            AppUser appUser = new()
            {
                Email = existingLawyer.EmailAddress,
                Role = "LAWYER"
            };

            string jwtToken = _jwtService.CreateToken(appUser);

            LawyerAuthResponse lawyerAuthResponse = new()
            {
                Lawyer = existingLawyer,
                AuthToken = jwtToken
            };

            serviceResponse.Response = lawyerAuthResponse;
            return serviceResponse;
        }

        public async Task<ServiceResponse<LawyerAuthResponse>> SignUp(LawyerAuthInput authInput)
        {
            ServiceResponse<LawyerAuthResponse> serviceResponse = new ServiceResponse<LawyerAuthResponse>();

            if (_context.Lawyer is null)
            {
                serviceResponse.Error = "Lawyer Context not found";
                return serviceResponse;
            }

            Lawyer existingLawyer = await _context.Lawyer.FirstOrDefaultAsync(c => c.EmailAddress == authInput.EmailAddress);

            if (existingLawyer is not null)
            {
                serviceResponse.Error = "A lawyer with the same email address already exists!";
                return serviceResponse;
            }

            _passwordService.CreatePasswordHash(authInput.Password, out byte[] passwordHash, out byte[] passwordSalt);

            Lawyer newLawyer = new()
            {
                Name = authInput.Name,
                EmailAddress = authInput.EmailAddress,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Address = authInput.Address,
                PhoneNumber = authInput.PhoneNumber,
                Qualification = authInput.Qualification,
                Type = authInput.Type
            };

            _context.Lawyer.Add(newLawyer);
            await _context.SaveChangesAsync();

            AppUser appUser = new()
            {
                Email = newLawyer.EmailAddress,
                Role = "LAWYER"
            };

            string jwtToken = _jwtService.CreateToken(appUser);

            LawyerAuthResponse lawyerAuthResponse = new()
            {
                Lawyer = newLawyer,
                AuthToken = jwtToken
            };

            serviceResponse.Response = lawyerAuthResponse;
            return serviceResponse;
        }
    }
}
