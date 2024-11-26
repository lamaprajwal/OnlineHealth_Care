using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineHealthCare.Domain.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Doctor's First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Doctor's Last Name")]
        public string LastName { get; set; }
        /// <summary>
        /// /[Display(Name = "New Appointments Disabled")]
        /// </summary>
        ///public Boolean DisableNewAppointments { get; set; }
        [Required]
        public string speciality {  get; set; }

        public string? DoctorImage {  get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
        


       


       


       
