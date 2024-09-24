using System.ComponentModel.DataAnnotations;

namespace decor.Models
{
    public class Cart
    {
        [Key]

        public int Cart_id { get; set; }
        public string Std_email { get; set; }
        public string Product_name { get; set; }
        public int Product_quantity { get; set; }
        public decimal Product_price { get; set; }
        public decimal Product_total_price { get; set; }
    }
}
