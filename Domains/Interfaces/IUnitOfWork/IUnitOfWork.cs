using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IBusinessRepository;
using Domains.Interfaces.IGenericRepository;
using Domains.Models;

namespace Domains.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository Products { get;  }
        public IGenericRepository<ProductBrand> ProductBrand { get;  }
        public IGenericRepository<Category> Categories { get;  }
        public IGenericRepository<OrderDeliveryMethods> DeliveryMethods { get; }
        public IGenericRepository<Order> Orders { get; }

        public Task<int> Save();
    }
}
