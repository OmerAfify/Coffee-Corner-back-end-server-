using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;

namespace Domains.Interfaces.IServices
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUser applicationUser);

    }
}
