using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.RequestParameters
{
    public class RequestParams
    {
        const int MaxPageSize = 50;
        public int PageNumber 
        { 
            get { return _pageSize; } 
            set { _pageSize = (value <= 0) ?  1 : value; }
        } 

        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize :(value<=0) ? 1 : value; }
        }
    }
}
