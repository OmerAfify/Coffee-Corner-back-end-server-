using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BusinesssLogic.Data;
using CoffeeCorner.DTOs;
using CoffeeCorner.Models.Pagination;
using Domains.Filteting;
using Domains.Interfaces.IUnitOfWork;
using Domains.Models;
using Domains.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCorner.Controllers
{

    [ApiController]
    [Route("api/[action]")]



    public class ProductController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
                
        }

       [HttpGet]
       public async Task<IActionResult> GetProducts([FromQuery] RequestParams requestParams)
        {
            return Ok(
               
                _mapper.Map<List<ProductDTO>>(await _unitOfWork.Products.GetAllAsync(
                new List<string>() {"Category","ProductBrand"},
                 q=> q.OrderBy(c=>c.ProductBrandId),requestParams
                ))
                
                );
        }



        [HttpGet]
        public async Task<IActionResult> GetProductsByFiltering ([FromQuery] FilteringObject filteringObject)
        {
            
            var data = _unitOfWork.Products.GetProdutsByFiltration(filteringObject);

            var dataDTO = new Pagination<ProductDTO>()
            {
                count = data.Result.count,
                pageNumber = data.Result.pageNumber,
                pageSize = data.Result.pageSize,
                data = _mapper.Map<List<ProductDTO>>(data.Result.data),

            };

            return Ok(dataDTO);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {

            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                return NotFound();
            else
            return Ok(_mapper.Map<ProductDTO>(product));
        }



        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetProductByCategoryId(int categoryId)
        {
            return Ok(_mapper.Map<List<ProductDTO>>(await _unitOfWork.Products.FindRangeAsync(c=>c.CategoryId==categoryId)));
        }

        [HttpGet("{productBrandId}")]
        public async Task<IActionResult> GetProductByBrandId(int productBrandId)
        {
            return Ok(_mapper.Map<List<ProductDTO>>(await _unitOfWork.Products.FindRangeAsync(c => c.ProductBrandId== productBrandId)));
        }

        [HttpGet("{productBrandId}/{categoryId}")]
        public async Task<IActionResult> GetProductByBrandAndCategoryId(int productBrandId, int categoryId)
        {
            return Ok(_mapper.Map<List<ProductDTO>>(await _unitOfWork.Products.FindRangeAsync(c => c.ProductBrandId == productBrandId && c.CategoryId==categoryId)));
        }




    }
}
