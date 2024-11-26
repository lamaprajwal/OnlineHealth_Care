using Application.Interfaces.Repositories.PatientRepository;
using onlineHealthCare.Database;
using onlineHealthCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.PatientRepository
{
    public class PatientRepository:Repository<ApplicationUser>,IPatientRepository
    {
        private readonly onlineHealthCareDbContext _applicationDbContext;
        public PatientRepository(onlineHealthCareDbContext dbContext) :base(dbContext)
        {
            this._applicationDbContext = dbContext;
        }
    }
}
