using Microsoft.AspNetCore.Mvc;
using FindALawyer.Data;
using FindALawyer.Services.ClientAuthService;
using FindALawyer.Dao;
using FindALawyer.Dao.ClientDao;

namespace FindALawyer.Controllers
{
    [Route("api/auth/client")]
    [ApiController]
    public class ClientsAuthController : ControllerBase
    {
        private readonly FindALawyerContext _context;
        private readonly IClientAuthService _clientAuthService;

        public ClientsAuthController(FindALawyerContext context, IClientAuthService clientAuthService)
        {
            _context = context;
            _clientAuthService = clientAuthService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<ServiceResponse<ClientAuthResponse>>> ClientSignUp([FromBody] ClientAuthInput clientAuthInput)
        {
            ServiceResponse<ClientAuthResponse> authResponse = await _clientAuthService.SignUp(clientAuthInput);

            if(authResponse.Error != null) return BadRequest(authResponse.Error);

            return Ok(authResponse.Response);

        }

        [HttpPost("login")]

        public async Task<ActionResult<ServiceResponse<ClientAuthResponse>>> ClientLogin([FromBody] LoginInput loginInput)
        {
            ServiceResponse<ClientAuthResponse> authResponse = await _clientAuthService.Login(loginInput);

            if (authResponse.Error != null) return BadRequest(authResponse.Error);

            return Ok(authResponse.Response);
        }
    }
}
