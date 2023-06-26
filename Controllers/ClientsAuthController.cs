using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindALawyer.Data;
using FindALawyer.Models;
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
    }
}
