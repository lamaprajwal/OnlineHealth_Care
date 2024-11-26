using Application.Interfaces.Repositories.DoctorRepository;
using Application.Interfaces.Services;
using Application.Interfaces.Services.DoctorService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using onlineHealthCare.Application.Dtos;
using onlineHealthCare.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository docRepo;
        public string _doctorimagePath = "wwwroot/Doctor/images";

        public DoctorService(IDoctorRepository docRepo)
        {
            this.docRepo = docRepo;
        }
        public async Task<DoctorDto> AddDoctorAsync(DoctorDto request)
        {
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                speciality = request.speciality,
                DoctorImage = await UploadImage(request.DoctorImage, request.FirstName)

            };
            docRepo.AddAsync(doctor);

            return request;
            
        }

        public async Task<List<DoctorResponse>> GetAll()
        {
            var doctorList = await docRepo.Queryable.ToListAsync();
            List<DoctorResponse> DoctotLists=doctorList
                .Select(doclist=>new DoctorResponse
                {
                    FirstName=doclist.FirstName,
                    LastName=doclist.LastName,
                       speciality=doclist.speciality,
                       DoctorImage=doclist.DoctorImage
                }).ToList();
            return DoctotLists;

        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_doctorimagePath, image), FileMode.Open, FileAccess.Read);
        }

        public async Task<IEnumerable<DoctorResponse>> SearchDoctorsAsync(string? name, string? specialty)
        {
            var query = docRepo.Queryable;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.FirstName.Contains(name) || d.LastName.Contains(name));
            }

            if (!string.IsNullOrEmpty(specialty))
            {
                query = query.Where(d => d.speciality.Contains(specialty));
            }

            var doctorList = await query.ToListAsync();
            var doctorResponse = doctorList.Select(d => new DoctorResponse
            {

                FirstName = d.FirstName,
                LastName = d.LastName,
                speciality = d.speciality,
                DoctorImage = d.DoctorImage
                // Map other properties as needed
            }); ;

            return doctorResponse;
        }



        public async Task<string> UploadImage(IFormFile? Img, string doctorName)
        {
            try
            {
                // Ensure doctor ID is provided
                if (string.IsNullOrEmpty(doctorName))
                    throw new ArgumentException("Doctor ID cannot be null or empty");

                // Build the save path, creating the directory if it doesn't exist
                var savePath = Path.Combine(_doctorimagePath, doctorName); // Creates a folder per doctor
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                // Generate a unique file name based on the current date and time
                var fileExtension = Path.GetExtension(Img.FileName);
                var fileName = $"doctor_{DateTime.Now:dd-MM-yyyy-HH-mm-ss}{fileExtension}";

                // Save the file to the specified path
                var filePath = Path.Combine(savePath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Img.CopyToAsync(fileStream);
                }

                // Construct the image URL to be returned and stored in the database
                var imageUrl = $"/content/doctors/{doctorName}/{fileName}";

                // Update the doctor's profile image URL in the database
                //using (var customContext = Context.CreateDbContext())
                //{
                //    var doctor = customContext.Doctors.FirstOrDefault(d => d.ID == doctorId);
                //    if (doctor == null)
                //        throw new Exception("Doctor not found");

                //    doctor.DoctorImage = imageUrl;
                //    //customContext.SaveChanges();
                //}

                return imageUrl; // Return the URL for further use
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error";
            }
        }
    }
}
