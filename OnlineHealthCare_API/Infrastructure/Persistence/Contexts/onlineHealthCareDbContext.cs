using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using onlineHealthCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static onlineHealthCare.Domain.Models.Appointment;

namespace onlineHealthCare.Database
{
   public class onlineHealthCareDbContext: IdentityDbContext<ApplicationUser>
    {
        public onlineHealthCareDbContext(DbContextOptions<onlineHealthCareDbContext> options) : base(options)
        {
            
        }
        public DbSet<Appointment> Appoinments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
           new IdentityRole { Id = "1", Name = "Patient", NormalizedName = "PATIENT" }
       );
          
        }
           

    }
    
}
