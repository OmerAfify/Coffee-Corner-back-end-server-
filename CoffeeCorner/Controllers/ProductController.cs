using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BusinesssLogic.Data;
using CoffeeCorner.DTOs;
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
        
        private IUnitOfWork _unitOfWork { get; }
        private IMapper _mapper{ get; }

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

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy;

            if (filteringObject.sortBy == "nameAsc")
                orderBy = q => q.OrderBy(p => p.ProductName);

            else if (filteringObject.sortBy == "nameDsc")
                orderBy = q => q.OrderByDescending(p => p.ProductName);

            else if (filteringObject.sortBy == "priceAsc")
                orderBy = q => q.OrderBy(p => p.SalesPrice);

            else if (filteringObject.sortBy == "priceDsc")
                orderBy = q => q.OrderByDescending(p => p.SalesPrice);
            else
                orderBy = null;

            Expression<Func<Product, bool>>  expression;



            if (filteringObject.categoryId == 0 && filteringObject.productBrandId == 0)
                expression = p => true;
            else if (filteringObject.categoryId != 0 && filteringObject.productBrandId != 0)
                expression = p => p.CategoryId == filteringObject.categoryId && p.ProductBrandId == filteringObject.productBrandId;
           else if (filteringObject.categoryId != 0 )
                expression = p => p.CategoryId == filteringObject.categoryId;
           
            else
                expression = p => p.ProductBrandId == filteringObject.productBrandId;


            var list =  _unitOfWork.Products.FindRangeAsync(expression,null, orderBy,filteringObject.requestParam);

            return Ok(_mapper.Map<List<ProductDTO>>(await list));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(_mapper.Map<ProductDTO>(await _unitOfWork.Products.GetByIdAsync(id)));
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
