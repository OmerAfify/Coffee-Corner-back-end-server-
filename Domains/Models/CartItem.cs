namespace Domains.Models
{
    public class CartItem
    {
        public int id { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }
        public string pictureUrl { get; set; }
        public string brand { get; set; }
        public string  type { get; set; }
    }
}