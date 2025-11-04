using AutoMapper;
using e_commerce_project.DTOs.User;
using e_commerce_project.Modles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace e_commerce_project.Controllers
{
   
    [Route("api/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public AuthController(UserManager<Users> _userManager,
            SignInManager<Users> _signInManager,
            IMapper _mapper,
            RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            mapper = _mapper;
            roleManager = _roleManager;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(UsersDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var NewUser = mapper.Map<Users>(user);
            NewUser.UserName = user.Email;
            IdentityResult result = await userManager.CreateAsync(NewUser, user.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);


            var roleresult = await userManager.AddToRoleAsync(NewUser, "User");
            if (!roleresult.Succeeded)
                return BadRequest(roleresult.Errors);

            await signInManager.SignInAsync(NewUser, isPersistent: false);
            string massage = $"Welcome aboard, {NewUser.First_Name}!";
            return Ok(massage);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginuser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = await userManager.FindByEmailAsync(loginuser.Email);

            if (user == null)
                return Unauthorized("Invalid Email or Password");

            var result = await signInManager.PasswordSignInAsync(user, loginuser.Password, loginuser.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized("Invalid Email or Password");

            string massage = $"Welcome back, {user.First_Name}!";
            return Ok(massage);

        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("AddAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdmin(UsersDto user)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var NewUser = mapper.Map<Users>(user);
            NewUser.UserName = user.Email;
            IdentityResult result = await userManager.CreateAsync(NewUser, user.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);


            var roleresult = await userManager.AddToRoleAsync(NewUser, "Admin");
            if (!roleresult.Succeeded)
                return BadRequest(roleresult.Errors);

            await signInManager.SignInAsync(NewUser, isPersistent: false);
            string massage = $"Welcome aboard, {NewUser.First_Name}!";
            return Ok(massage);
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(UserId);
            var userProfile = mapper.Map<UserProfileDTO>(user);
            return Ok(userProfile);
        }


    }

        public class UsersDto
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string First_Name { get; set; } = string.Empty;
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]

            public string Last_Name { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
            [Required]
            public DateOnly Birthdate { get; set; }

            [Required]
            public string Gender { get; set; }


        }
        public class LoginDto
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }
        public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "User", "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }
    }

}

