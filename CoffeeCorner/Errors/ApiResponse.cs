using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCorner.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public ApiResponse(int statusCode, string message = null )
        {
            StatusCode = statusCode;
            Message = message ?? getDefaultStatusCodeMessage(statusCode);

        }

        private string getDefaultStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request is sent.",
                401 => "You are unauthorized.",
                404 => "404 Not found.",
                500 => "Internal Server Error.",
                _ => null,
            };
        }
    }

}
