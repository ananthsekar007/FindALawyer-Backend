using FindALawyer.Dao;
using FindALawyer.Dao.ClientDao;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Services.JwtService;
using FindALawyer.Services.PasswordService;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FindALawyer.Services.ClientAuthService
{
    public class ClientAuthServiceImpl : IClientAuthService
    {

        private readonly FindALawyerContext _context;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public ClientAuthServiceImpl(FindALawyerContext context, IPasswordService passwordService, IJwtService jwtService) {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse<ClientAuthResponse>> Login(LoginInput loginInput)
        {
            ServiceResponse<ClientAuthResponse> serviceResponse = new ServiceResponse<ClientAuthResponse>();

            if (_context.Client is null)
            {
                serviceResponse.Error = "Client Context not found";
                return serviceResponse;
            }

            Client existingClient = await _context.Client.FirstOrDefaultAsync(c => c.EmailAddress == loginInput.Email);

            if(existingClient is null)
            {
                serviceResponse.Error = "No user exists with this email!";
                return serviceResponse;
            }

            if(!_passwordService.VerifyPasswordHash(loginInput.Password, existingClient.PasswordHash, existingClient.PasswordSalt))
            {
                serviceResponse.Error = "Please check the credentials and try again!";
                return serviceResponse;
            }

            AppUser appUser = new()
            {
                Email = existingClient.EmailAddress,
                Role = "CLIENT"
            };

            string jwtToken = _jwtService.CreateToken(appUser);

            ClientAuthResponse clientAuthResponse = new()
            {
                Client = existingClient,
                AuthToken = jwtToken
            };

            serviceResponse.Response = clientAuthResponse;
            return serviceResponse;

        }

        public async Task<ServiceResponse<ClientAuthResponse>> SignUp(ClientAuthInput authInput)
        {
            ServiceResponse<ClientAuthResponse> serviceResponse = new ServiceResponse<ClientAuthResponse>();

            if(_context.Client is null)
            {
                serviceResponse.Error = "Client Context not found";
                return serviceResponse;
            }

            Client existingClient = await _context.Client.FirstOrDefaultAsync(c => c.EmailAddress == authInput.EmailAddress);

            if(existingClient is not null) {
                serviceResponse.Error = "A client with the same email address already exists!";
                return serviceResponse;
            }

            _passwordService.CreatePasswordHash(authInput.Password, out byte[] passwordHash, out byte[] passwordSalt);

            Client newClient = new()
            {
                Name = authInput.Name,
                EmailAddress = authInput.EmailAddress,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Address = authInput.Address,
                PhoneNumber = authInput.PhoneNumber,
            };

            _context.Client.Add(newClient);
            await _context.SaveChangesAsync();

            AppUser appUser = new()
            {
                Email = newClient.EmailAddress,
                Role = "CLIENT"
            };

            string jwtToken = _jwtService.CreateToken(appUser);

            ClientAuthResponse clientAuthResponse = new()
            {
                Client = newClient,
                AuthToken = jwtToken
            };

            serviceResponse.Response = clientAuthResponse;
            return serviceResponse;

        }
    }
}
