using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCorner.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string PictureUrl { get; set; }
        public decimal SalesPrice { get; set; }
        public  string BrandName { get; set; }
        public  string CategoryName { get; set; }
    }
}
