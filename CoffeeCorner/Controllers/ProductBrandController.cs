using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinesssLogic.Data;
using CoffeeCorner.DTOs;
using Domains.Interfaces.IUnitOfWork;
using Domains.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCorner.Controllers
{

    [ApiController]
    [Route("api/[action]")]



    public class ProductBrandController : ControllerBase
    {

        private IUnitOfWork _unitOfWork { get; }
        private IMapper _mapper { get; }

        public ProductBrandController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetProductBrands()
        {
            return Ok(

                _mapper.Map<List<ProductBrandDTO>>(await _unitOfWork.ProductBrand.GetAllAsync())

                );
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductBrand(int id)
        {
            return Ok(_mapper.Map<ProductBrandDTO>(await _unitOfWork.ProductBrand.GetByIdAsync(id)));
        }



    }
}
