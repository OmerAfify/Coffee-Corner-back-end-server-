using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Extensions;
using CoffeeCorner.DTOs;
using Domains.Interfaces.IServices;
using Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeCorner.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper= mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUser() {


          try {    //extrntion method being used 
            var user = await  _userManager.GetUserWithClaimsPrincipal(User);

            //or
            // var email =  User.FindFirstValue(ClaimTypes.Email);
            // var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest();

            return Ok(new UserDTO()
            {
                Email = user.Email,
                Name = user.firstName,
                Token = _tokenService.CreateToken(user)
            });     
          } catch (Exception ex) { 
                return Problem("an error occured.");
            }
         
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress() {


            try {  //extrntion method being used 
            var user =await  _userManager.GetUserAddressWithClaimsPrincipal(User);


            if (user == null)
                return BadRequest();

            return Ok(_mapper.Map<AddressDTO>(user.address));
            }
            catch (Exception ex) {
                return Problem("an error occured."); 
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress([FromBody] AddressDTO addressDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest();


            try {
                var user = await _userManager.GetUserAddressWithClaimsPrincipal(User);

                user.address = _mapper.Map<Address>(addressDTO);

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                          return BadRequest();

               return Ok(_mapper.Map<AddressDTO>(user.address));
                

                }
            catch (Exception ex) { 
                return Problem(" error has occured. " + ex.Message); 
            
            }

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
