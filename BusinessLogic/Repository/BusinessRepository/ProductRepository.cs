using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinesssLogic.Data;
using BusinesssLogic.Repository.GenericRepository;
using CoffeeCorner.Models.Pagination;
using Domains.Filteting;
using Domains.Interfaces.IBusinessRepository;
using Domains.Interfaces.IGenericRepository;
using Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinesssLogic.Repository.BusinessRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private DataStoreContext _context { get; set; }

        public ProductRepository(DataStoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pagination<Product>> GetProdutsByFiltration(FilteringObject filteringObject)
        {
            IQueryable<Product> query = _context.Products;

            if (filteringObject.categoryId!=0) {
                query = query.Where(c=>c.CategoryId==filteringObject.categoryId);
            }
            
            if (filteringObject.productBrandId != 0)
            {
                query = query.Where(b=>b.ProductBrandId==filteringObject.productBrandId);
            }


            if ( !string.IsNullOrEmpty(filteringObject.sortBy ))
            {
                switch (filteringObject.sortBy)
                {
                        case "nameAsc":
                        query = query.OrderBy(q=>q.ProductName);
                        break;
                    case "nameDsc":
                        query = query.OrderByDescending(q => q.ProductName);
                        break;
                    case "priceAsc":
                        query = query.OrderBy(q => q.SalesPrice);
                        break;
                    case "priceDsc":
                        query = query.OrderByDescending(q => q.SalesPrice);
                        break;
                }

            }

            var pagingData = new Pagination<Product>();

            pagingData.count = query.Count();

            if (filteringObject.requestParam != null)
               query =   query.Skip((filteringObject.requestParam.PageNumber - 1) * filteringObject.requestParam.PageSize).Take(filteringObject.requestParam.PageSize);

            

            pagingData.pageNumber = (filteringObject.requestParam!=null)?filteringObject.requestParam.PageNumber:1;
            pagingData.pageSize= (filteringObject.requestParam != null) ? filteringObject.requestParam.PageSize: pagingData.count;
            pagingData.data = await query.AsNoTracking().ToListAsync();

            return pagingData;



        }
    }
}
