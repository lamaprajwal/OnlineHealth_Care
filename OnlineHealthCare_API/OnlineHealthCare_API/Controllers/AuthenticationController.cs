using Application.Interfaces.Services.PatientService;
using Microsoft.AspNetCore.Mvc;
using onlineHealthCare.Application.Dtos;

namespace OnlineHealthCare_API.Controllers
{
    public class AuthenticationController : Controller
    {
        public IPatientService patientService { get; set; }
        
        public AuthenticationController(IPatientService _patientService)
        {
            patientService = _patientService;
           
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Registeration([FromBody] RegisterModel Data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await patientService.Register(Data);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        
    }
}

