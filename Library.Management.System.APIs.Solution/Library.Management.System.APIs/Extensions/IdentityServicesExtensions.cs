using Library.Management.System.Core.Entities;
using Library.Management.System.Repository.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.Management.System.APIs.Extensions
{
    // Extension methods for adding identity services to the IServiceCollection
    public static class IdentityServicesExtensions
    {
        // Extension method to add identity services and JWT authentication
        public static IServiceCollection AddIdentityServices(this IServiceCollection services ,IConfiguration configuration)
        {
            // Configure Identity services with custom options

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                //// Password settings
                //options.Password.RequireDigit = false;
                //options.Password.RequiredLength = 6;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireLowercase = false;
                //options.Password.RequiredUniqueChars = 1;


            }).AddEntityFrameworkStores<AppDbContext>();

            // Configure authentication services
            services.AddAuthentication(options=>
                                        {
                                      // Set default authentication scheme to JWT Bearer
                                      options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                                      // Set default challenge scheme to JWT Bearer (to any endpoint)
                                      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                                        })
                                        .AddJwtBearer(options =>
                                        {
                                            // Configure JWT Bearer authentication handler
                                            options.TokenValidationParameters = new TokenValidationParameters()
                                            {
                                                ValidateAudience = true,
                                                ValidAudience = configuration["JWT:ValidAudience"],
                                                ValidateIssuer=true,
                                                ValidIssuer = configuration["JWT:ValidIssuer"],
                                                ValidateIssuerSigningKey =true,
                                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                                                ValidateLifetime =true,
                                                ClockSkew =TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"]))



                                            };
                                        });

            return services;  // Return the IServiceCollection for chaining
        }
    }
}
