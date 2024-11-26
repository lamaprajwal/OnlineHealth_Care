using Application.Interfaces.Services.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services.PatientService;
using onlineHealthCare.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using onlineHealthCare.Database;
using onlineHealthCare.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Infrastructure.Persistence.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly onlineHealthCareDbContext _context;

        public PatientService(UserManager<ApplicationUser> userManager, IConfiguration configuration, onlineHealthCareDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }
        public async Task<AuthModel> Register(RegisterModel model)
        {
            try
            {
                var result = await RegisterAsync(model);
                if (!result.IsAuthenticated)
                {
                    throw new Exception(result.Message);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new AuthModel { Message = ex.Message };
            }
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            model.Name = model.Name.Replace(" ", "-");

            // Check if email or username is already registered
            if (await _userManager.FindByEmailAsync(model.EmailAddress) != null)
            {
                return new AuthModel { Message = "Email is already registered!" };
            }

            if (await _userManager.FindByNameAsync(model.Name) != null)
            {
                return new AuthModel { Message = "Username is already registered!" };
            }

            // Create new user
            var user = new ApplicationUser
            {
                UserName = model.Name,
                Name = model.Name,
                Email = model.EmailAddress,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber
            };

            var creationResult = await _userManager.CreateAsync(user, model.Password);
            if (!creationResult.Succeeded)
            {
                var errors = string.Join(",", creationResult.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            // Assign role to the user
            await _userManager.AddToRoleAsync(user, "Patient");

            // Generate JWT token
            var jwtToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Id = user.Id,
                Email = user.Email,
                ExpiresOn = jwtToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Patient" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Username = user.Name
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            // Fetch user claims and roles
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            // Create role claims
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            // Combine claims
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }.Union(userClaims).Union(roleClaims);

            // Configure JWT settings
            var key = _configuration["JWT:key"];
            var issuer = _configuration["JWT:issuer"];
            var audience = _configuration["JWT:Audience"];
            var durationInDays = double.Parse(_configuration["JWT:DurationInDays"]);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Create JWT token
            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(durationInDays),
                signingCredentials: signingCredentials
            );
        }
    }
}

