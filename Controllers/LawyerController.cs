using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Dao.LawyerDao;
using FindALawyer.Dao;
using FindALawyer.Services.LawyerAuthService;
using FindALawyer.Services.LawyerService;
using Microsoft.AspNetCore.Authorization;

namespace FindALawyer.Controllers
{
    [Route("api/lawyer")]
    [ApiController]
    public class LawyerController : ControllerBase

    {
        private readonly ILawyerService _lawyerService;
        public LawyerController(ILawyerService lawyerService)
        {
            _lawyerService = lawyerService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<ServiceResponse<ICollection<LawyerWithRatings>>>> GetLawyers()
        {
            ServiceResponse<ICollection<LawyerWithRatings>> lawyers = await _lawyerService.GetAllLawyersWithRatings();

            return Ok(lawyers.Response);
        }

        [HttpPost("rate")]
        public async Task<ActionResult<ServiceResponse<string>>> RateALawyer(Rating ratingInput)
        {
            ServiceResponse<string> response = await _lawyerService.RateALawyer(ratingInput);
            if(response.Error != null) return BadRequest(response.Error);
            return Ok(response.Response);

        }

    }
}
