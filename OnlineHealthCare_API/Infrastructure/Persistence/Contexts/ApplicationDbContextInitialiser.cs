
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using onlineHealthCare.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly onlineHealthCareDbContext _context;
       
        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
           onlineHealthCareDbContext context)
        {
            _logger = logger;
            _context = context;
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task InitialiseAsync()
        {
            try
            {
               _context.Database.EnsureCreated();
                if (_context.Database.IsSqlServer())
                {
                    //await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        /// <summary>
        /// This mothod seed the data
        /// </summary>
        /// <returns></returns>
        //public async Task SeedAsync()
        //{
        //    try
        //    {
        //        //if (!await _context.Product.AnyAsync())
        //        //{
        //        //    var assembly = Assembly.GetExecutingAssembly();
        //        //    var resourceNames = assembly.GetManifestResourceNames().Where(str => str.EndsWith(".sql"));
        //        //    //foreach (string resourceName in resourceNames)
        //        //    //{
        //        //    //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //        //    //    using (StreamReader reader = new StreamReader(stream))
        //        //    //    {
        //        //    //        string sql = reader.ReadToEnd();
        //        //    //        await _context.Database.ExecuteSqlRawAsync(sql, new object[0]);
        //        //    //    }
        //        //    //}
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while seeding the database.");
        //        throw;
        //    }
        //}
    }
}

