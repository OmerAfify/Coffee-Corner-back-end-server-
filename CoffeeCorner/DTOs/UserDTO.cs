using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCorner.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDTO : LoginDTO
    {
        public string FirstName { get; set; }
        public string LasttName { get; set; }
    }
    
    public class UserDTO 
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }


}
