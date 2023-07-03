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
using FindALawyer.Services.LawyerAuthService;
using FindALawyer.Dao.ClientDao;
using FindALawyer.Dao;
using FindALawyer.Dao.LawyerDao;

namespace FindALawyer.Controllers
{
    [Route("api/auth/lawyer")]
    [ApiController]
    public class LawyersAuthController : ControllerBase
    {
        private readonly ILawyerAuthService _lawyerAuthService;

        public LawyersAuthController(ILawyerAuthService lawyerAuthService)
        {
            _lawyerAuthService = lawyerAuthService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<ServiceResponse<LawyerAuthResponse>>> ClientSignUp([FromBody] LawyerAuthInput lawyerAuthInput)
        {
            ServiceResponse<LawyerAuthResponse> authResponse = await _lawyerAuthService.SignUp(lawyerAuthInput);

            if (authResponse.Error != null) return BadRequest(authResponse.Error);

            return Ok(authResponse.Response);

        }

        [HttpPost("login")]

        public async Task<ActionResult<ServiceResponse<LawyerAuthResponse>>> ClientLogin([FromBody] LoginInput loginInput)
        {
            ServiceResponse<LawyerAuthResponse> authResponse = await _lawyerAuthService.Login(loginInput);

            if (authResponse.Error != null) return BadRequest(authResponse.Error);

            return Ok(authResponse.Response);
        }

    }
}
