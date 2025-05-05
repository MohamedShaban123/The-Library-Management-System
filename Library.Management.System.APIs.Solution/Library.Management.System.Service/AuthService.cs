using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;


        public AuthService(IConfiguration configuration , RoleManager<IdentityRole<int>> roleManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
        }



        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {

            // private Claims (User-Defined)
            var authClaims = new List<Claim>()
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.GivenName,user.UserName),
              new Claim(ClaimTypes.Email,user.Email) ,
              
            };

            //this for add role of each user to token
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role,role));
            }
           
            // Verify signature
            var Authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            // token object
            var token = new JwtSecurityToken(
                // Registerd Claims
                audience: _configuration["JWT:ValidAudience"],
                // Registerd Claims
                issuer: _configuration["JWT:ValidIssuer"],
                // Registerd Claims,
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                // Private Claims,
                claims: authClaims,
                // Private Claims,
                signingCredentials: new SigningCredentials(Authkey,SecurityAlgorithms.HmacSha256Signature)

                );

            // create token
            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }


    }
}
