using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineHealthCare.Domain.Models
{
    public class HealthTest
    {
        [Key]
        public int HealthTestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TestName { get; set; }  // Name of the test, e.g., "Blood Test"

        [MaxLength(500)]
        public string Description { get; set; }  // Description or notes for the test
        public DateTime RequestedDate { get; set; }  // Date the test was requested

        public string PatientId { get; set; }
        [ForeignKey("PatientId")]
        public ApplicationUser Patient { get; set; }

    }
}
       

       

