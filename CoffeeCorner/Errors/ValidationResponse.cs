using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCorner.Errors
{
    public class ValidationResponse : ApiResponse
    {

        public IEnumerable<string> Errors { get; set; }

        public ValidationResponse():base(400)
        {

        }
    }
}
