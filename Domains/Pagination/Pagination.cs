using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCorner.Models.Pagination
{
    public class Pagination<T> where T : class
    {
        public int count { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }

        public  IEnumerable<T> data { get; set; }

    }
}
