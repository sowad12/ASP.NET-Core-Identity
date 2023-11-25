
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAuth.Library.Models.ViewModels;

namespace UserAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                               IConfiguration configuration)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _configuration=configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            var userExist= await _userManager.FindByEmailAsync(model.Email);
            if (userExist!=null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response
                {
                   status="Error",
                    message="user alreadyExist"
                });
                    
            }
            IdentityUser identityUser =new ()
            {
                Email=model.Email, 
                UserName=model.Name

            };
            var IsRoleExist = await _roleManager.RoleExistsAsync(model.Role);
            if (IsRoleExist)
            {
                var res = await _userManager.CreateAsync(identityUser, model.Password);
                if (res.Succeeded)
                {
                  
                   await _userManager.AddToRoleAsync(identityUser, model.Role);
                    return StatusCode(StatusCodes.Status201Created, new Response
                    {
                        status = "Success",
                        message = "user Created"
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        status = "Failed",
                        message = "user failed to create"
                    });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    status = "Failed",
                    message = "Role Is not Exist"
                });
            }
           
            
           
        }
    }
}
