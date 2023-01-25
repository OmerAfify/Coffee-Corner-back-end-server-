﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;
using Domains.RequestParameters;

namespace Domains.Interfaces.IGenericRepository
{
    public interface  IGenericRepository<T>  where T : BaseEntity
    {

        public  Task<T> GetByIdAsync(int id);

        //Includes List Must have the exact Domain class Name
        //OrderBy exp is passed as arg like this ex: q => q.OrderBy(c=>c.ProductBrandId)

        public Task<IEnumerable<T>> GetAllAsync(List<string> includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, RequestParams requestParams=null);
        public  Task<T> FindAsync(Expression<Func<T, bool>> predicate, List<string> includes = null);
        public  Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate, List<string> includes = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, RequestParams requestParams = null);
        public  void InsertAsync(T entity);
        public  void InsertRangeAsync(List<T> entities);
        public void Delete(T entity);
        public void DeleteRange(List<T> entities);
        public void Update(T entity);
       

    }
}
