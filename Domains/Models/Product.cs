using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class Product : BaseEntity
    {

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string PictureUrl { get; set; }
        public decimal SalesPrice { get; set; }
      

        public int  ProductBrandId  { get; set; }
        public virtual ProductBrand  ProductBrand  { get; set; }
        public int CategoryId { get; set; }
        public virtual Category  Category  { get; set; }
      
      

    
    }

  
}
