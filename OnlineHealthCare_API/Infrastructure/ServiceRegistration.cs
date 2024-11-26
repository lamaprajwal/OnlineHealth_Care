using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.DoctorRepository;
using Application.Interfaces.Services;
using Application.Interfaces.Services.DoctorService;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.DoctorRepository;
using Infrastructure.Persistence.Services.DoctorService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IDoctorService,DoctorService>();


            services.AddTransient(typeof(Repository<>), typeof(Repository<>));

            services.AddTransient<IDoctorRepository,DoctorRepository>();
        }
    }
}
