using Application.Interfaces.Services.DoctorService;
using Microsoft.AspNetCore.Mvc;
using onlineHealthCare.Application.Dtos;

namespace OnlineHealthCare_API.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpPost("RegisterDoctor")]
        public async Task<IActionResult> RegsiterDoctor(DoctorDto doc)
        {
            _service.AddDoctorAsync(doc);
            return Ok(doc);

        }
        //[HttpPost("UploadImg")]
        //public async Task<IActionResult> UploadImage(IFormFile? file,string doctorId)
        //{
        //    await _service.UploadImage(file, doctorId);
        //    return Ok(file);
        //}
        [HttpGet("search")]
        public async Task<IActionResult> SearchDoctors([FromQuery] string? name = null, [FromQuery] string? specialty = null)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(specialty))
            {
                return BadRequest("At least one search parameter (name or specialty) is required.");
            }

            var doctors = await _service.SearchDoctorsAsync(name, specialty);

            if (doctors == null || !doctors.Any())
            {
                return NotFound("No doctors found.");
            }

            return Ok(doctors);
        }
        [HttpGet("GetAllDoctors")]

        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _service.GetAll();
            return Ok(doctors);
        }
    }
}
