using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineHealthCare.Application.Dtos
{
    public class DoctorResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string speciality { get; set; }

        public string DoctorImage { get; set; }
    }
}
