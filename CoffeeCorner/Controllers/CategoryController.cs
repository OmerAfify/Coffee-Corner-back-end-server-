using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinesssLogic.Data;
using CoffeeCorner.DTOs;
using CoffeeCorner.Errors;
using Domains.Interfaces.IUnitOfWork;
using Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCorner.Controllers
{

    [ApiController]
    [Route("api/[action]")]



    public class CategoryController : ControllerBase
    {

        private IUnitOfWork _unitOfWork { get; }
        private IMapper _mapper { get; }

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        [Authorize]
        
        public async Task<IActionResult> GetCategories()
        {
         
            return Ok(  _mapper.Map<List<CategoryDTO>>(await _unitOfWork.Categories.GetAllAsync()));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);

                if(category==null) 
                    return NotFound(new ApiResponse(404));

                return Ok(_mapper.Map<ProductDTO>(category));

            }catch(Exception ex)
            {
                return StatusCode(500, new ExceptionResponse(500,null, ex.Message ));
            }

            }



    }
}
