using Application.Interfaces.Repositories.DoctorRepository;
using onlineHealthCare.Database;
using onlineHealthCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.DoctorRepository
{
    public class DoctorRepository: Repository<Doctor>, IDoctorRepository
    {
        private readonly onlineHealthCareDbContext _applicationDbContext;
        public DoctorRepository(onlineHealthCareDbContext dbContext) : base(dbContext)
        {
            this._applicationDbContext = dbContext;
        }
    }
}
