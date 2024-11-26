using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineHealthCare.Application.Dtos
{
    public class DoctorDto
    {

        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? speciality {  get; set; }

        public IFormFile DoctorImage { get; set; }
    }
}
