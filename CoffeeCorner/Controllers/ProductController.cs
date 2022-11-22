using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinesssLogic.Data;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCorner.Controllers
{

    [ApiController]
    [Route("api/[action]")]



    public class ProductController : ControllerBase
    {
        
        private DataStoreContext _context { get; }

        public ProductController(DataStoreContext datasStoreContext)
        {
            _context = datasStoreContext;
                
        }

       [HttpGet]
       public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }
        


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(await _context.Products.FindAsync(id));
        }



    }
}
