using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineHealthCare.Domain.Models
{
    public  class ApplicationUser:IdentityUser
    {

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "User Name")]
        public string Name { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber {  get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        //for patients

        public ICollection<HealthTest> HealthTest { get; set; }

    }
}






