using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCorner.Errors
{
    public class ExceptionResponse : ApiResponse
    {
      
        public string Details { get; set; }

        public ExceptionResponse(int statusCode, string message = null , string details = null ):base(statusCode, message)
        {
            Details = details;
        }

       
    }
}
