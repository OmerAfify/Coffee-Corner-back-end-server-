using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinesssLogic.Data;
using BusinesssLogic.Repository.BusinessRepository;
using BusinesssLogic.Repository.GenericRepository;
using Domains.Interfaces.IBusinessRepository;
using Domains.Interfaces.IGenericRepository;
using Domains.Interfaces.IUnitOfWork;
using Domains.Models;

namespace BusinessLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataStoreContext _context;
       
        public IProductRepository Products { get; }
        public IGenericRepository<ProductBrand> ProductBrand { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<OrderDeliveryMethods> DeliveryMethods { get; }
        public IGenericRepository<Order> Orders { get; }
   
        public UnitOfWork(DataStoreContext context)
        {
            _context = context;

            Products = new ProductRepository(_context);
            ProductBrand = new GenericRepository<ProductBrand>(_context);
            Categories= new GenericRepository<Category>(_context);
            DeliveryMethods= new GenericRepository<OrderDeliveryMethods>(_context);
            Orders= new GenericRepository<Order>(_context);


        }

     
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
