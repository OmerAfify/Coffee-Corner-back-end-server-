using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeCorner.DTOs;
using Domains.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeCorner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterDTO registerUserDTO) {

            if (!ModelState.IsValid)
                return BadRequest();

            try {

                var  user = new ApplicationUser() {

                    Email = registerUserDTO.Email,
                    UserName = registerUserDTO.Email,
                    firstName = registerUserDTO.FirstName,
                    lastName = registerUserDTO.LasttName
                };

                var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

                if (result.Succeeded)
                {
                    return new UserDTO()
                    {
                        Email = user.Email,
                       Name = user.firstName,
                        Token = "stokeen"
                    };

                }
                else
                {
                    return BadRequest();
                }
            
            }catch(Exception ex)
            {
                return Problem("500 error");

            }
        
        }

        //[HttpPost]
        //public Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO loginUserDTO) { }

    }
}
