using Library.Management.System.APIs.Dtos;
using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Management.System.APIs.Controllers
{
    // Controller for managing user accounts
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public SignInManager<ApplicationUser> _signInManager { get; }

        // Constructor to initialize dependencies
        public AccountController(UserManager<ApplicationUser> userManager 
            , SignInManager<ApplicationUser> signInManager,
            IAuthService authService ,
          RoleManager<IdentityRole<int>> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _roleManager = roleManager;
            
        }

        // Endpoint for user login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]  LoginDto model)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
          
            if (user == null)
                return Unauthorized("Login Failed");
            // This Method Just Check password (model.password) and password of  user that stored in Database OK 
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Login Failed");
            // Generate token
            var Token = await _authService.CreateTokenAsync(user, _userManager);
            return Ok(Token);
        }

        // Endpoint for user registration
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDto model)
        {

            // Check if a user with the same email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest("Email already exists");
            }
            // Create new user
            var user = new ApplicationUser()
            {
                UserName = model.UserName.Split("@")[0],
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                
            };

            // _userManager.CreateAsync(user, model.Password) automatically hashes the password before storing it in the database.
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                // Collect all errors and return them in the response
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Message = "Register Failed", Errors = errors });
            }

            // Assign "User" role to the new user
            if (!await _userManager.IsInRoleAsync(user, "User"))
            await _userManager.AddToRoleAsync(user, "User");
            
           
            return Ok("Registered Successfully.");

        }


        //// Endpoint to add a role to a user, accessible only by Admins
        [Authorize(Roles ="User")]
        [HttpPost("AssignRoleToSpecificUser")]
        // This EndPoint Just Add Role to User NOt create ok
        public async Task<ActionResult<string>> AddRoleAsync([FromBody] RoleDto model)
        {   // Find user by username
            var user = await _userManager.FindByNameAsync(model.userName);
            if (user is null )
                return "Invalid UserName";
            // Check if role exists
            if (!await _roleManager.RoleExistsAsync(model.roleName))
                return BadRequest("Role not  exist");
            // Check if user already has the role
            if (await _userManager.IsInRoleAsync(user, model.roleName))
                return "Role already Assigned To This User";

            // Add role to user
            var result = await _userManager.AddToRoleAsync(user, model.roleName);
            return result.Succeeded? "Role Added Successfully" :"Error To Add Role";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{roleName}")]
        public async Task<ActionResult<string>> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
                return BadRequest(new { message = "Role already exists" });

            var result = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));

            if (result.Succeeded)
                return Ok(new { message = $"Role {roleName} created successfully!" });

            return BadRequest(result.Errors);
        }

    }
}
