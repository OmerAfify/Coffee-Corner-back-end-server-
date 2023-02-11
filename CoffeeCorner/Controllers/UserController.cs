using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeCorner.DTOs;
using Domains.Interfaces.IServices;
using Domains.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeCorner.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        ITokenService _tokenService;
        public UserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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
                        Token = _tokenService.CreateToken(user)
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

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO loginUserDTO) {

            if (!ModelState.IsValid)
                return BadRequest();

            try {

                var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
                
                if (user == null)
                    return BadRequest();
                
                var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Email,loginUserDTO.Password, false, false);
           
                if (result.Succeeded) { 
                    return new UserDTO() { 
                        Email = loginUserDTO.Email,
                        Name = user.firstName, 
                        Token =_tokenService.CreateToken(user)
                    };
                }
                else
                    return BadRequest();

            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message+ "error ff");
            }
        
        }


      

    }
}
