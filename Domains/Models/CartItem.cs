using System.ComponentModel.DataAnnotations;

namespace Domains.Models
{
    public class CartItem
    {
        public int id { get; set; }
        
        [Required]
        public string productName { get; set; }

        [Required]
        [Range(0.1,10000, ErrorMessage ="Price should be greater than 0 and atmost 10000")]
        public decimal price { get; set; }

        [Required]
        [Range(1,100,ErrorMessage ="Quantity must be atleast 1 and atmost 100.")]
        public int qty { get; set; }


        public string pictureUrl { get; set; }
       
        [Required]
        public string brand { get; set; }
        [Required]
        public string  type { get; set; }
    }
}