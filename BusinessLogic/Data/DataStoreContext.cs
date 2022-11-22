using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace BusinesssLogic.Data
{
    public class DataStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DataStoreContext(DbContextOptions<DataStoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);

        }

    }
}
