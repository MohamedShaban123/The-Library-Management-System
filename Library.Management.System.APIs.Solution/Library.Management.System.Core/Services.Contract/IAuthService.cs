using Library.Management.System.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Core.Services.Contract
{
    // Interface for authentication services
    public interface IAuthService
    {
        // Method to create a JWT token for a given user
        Task<string> CreateTokenAsync(ApplicationUser user ,UserManager<ApplicationUser> userManager);

    }
}
