using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class Address
    {
        public int id{ get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string street { get; set; }
        public string ZipCode { get; set; }

       [Required]
        public string applicationUserId{ get; set; }
        public ApplicationUser applicationUser{ get; set; }
    }
}
