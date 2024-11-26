using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineHealthCare.Domain.Models
{
    public class Appointment
    {
       
            [Key]
            public int AppointmentID { get; set; }

            [Required]
            public string DoctorId { get; set; }

            public string? PatientId { get; set; }

            [ForeignKey("DoctorId")]
            public Doctor Doctor { get; set; }

            [ForeignKey("PatientId")]
            public ApplicationUser? Patient { get; set; }

            [Required]
            public DateOnly AppointmentDateTime { get; set; }

             [Required]
             public TimeOnly TimeStart { get; set; }
             public TimeOnly? TimeEnd { get; set; }
            [Required]
            [MaxLength(20)]
            public string Status { get; set; } // e.g., Available, scheduled, canceled, completed
            [MaxLength(250)]
            public string? Notes { get; set; }
    }
}

       

              


