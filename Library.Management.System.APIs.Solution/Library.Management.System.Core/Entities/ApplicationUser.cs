using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Core.Entities
{
    // Represents an application user in the system
    // Inherits from IdentityUser with an integer key
    public class ApplicationUser : IdentityUser<int>
    {

        // Additional properties specific to the application user can be added here

    }
}
