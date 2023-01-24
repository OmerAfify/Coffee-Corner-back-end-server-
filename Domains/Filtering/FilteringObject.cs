using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.RequestParameters;

namespace Domains.Filteting
{
    public class FilteringObject
    {
        public string  sortBy { get; set; }
        public int categoryId { get; set; }
        public int productBrandId { get; set; }
        public RequestParams requestParam { get; set; }
    }
}
