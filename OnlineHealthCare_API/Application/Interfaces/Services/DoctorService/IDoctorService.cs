using Microsoft.AspNetCore.Http;
using onlineHealthCare.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<string> UploadImage(IFormFile? Img, string doctorId);
        FileStream ImageStream(string image);
        Task<IEnumerable<DoctorResponse>> SearchDoctorsAsync(string? name, string? specialty);
        Task<List<DoctorResponse>> GetAll();

        public Task<DoctorDto> AddDoctorAsync(DoctorDto request);
    }
}
