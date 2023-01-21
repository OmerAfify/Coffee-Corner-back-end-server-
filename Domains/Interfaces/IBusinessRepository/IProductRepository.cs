using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using Domains.Models;

namespace Domains.Interfaces.IBusinessRepository
{
    public interface IProductRepository : IGenericRepository<Product> 
    {
      
    }

}
