using Library.Management.System.APIs.Extensions;
using Library.Management.System.APIs.Helpers;
using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Repositories.Contract.IGenericRepository;
using Library.Management.System.Core.Services.Contract;
using Library.Management.System.Core.UnitOfWorks.Contract;
using Library.Management.System.Repository.Data.Contexts;
using Library.Management.System.Repository.Repositories.GenericRepository;
using Library.Management.System.Repository.UnitOfWorks;
using Library.Management.System.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; 
using System.Reflection;
using System.Threading.Tasks;

namespace Library.Management.System.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddEndpointsApiExplorer();

            // Configure Swagger/OpenAPI
            webApplicationBuilder.Services.AddSwaggerGen(options =>
            {
                // Add JWT Bearer Authorization to Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token like this: Bearer {your token here}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            #region User Configure Services

            webApplicationBuilder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("LibraryConnectionToDatabase"));
            });

            webApplicationBuilder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfile));

            webApplicationBuilder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));

            webApplicationBuilder.Services.AddMemoryCache();

            #endregion

            var app = webApplicationBuilder.Build();

            #region Update Database and Data Seeding

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<AppDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await DataSeedContext.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error To Add Migrations");
            }

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
