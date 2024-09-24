using System.ComponentModel.DataAnnotations;

namespace decor.Models
{
    public class Checkout
    {
        [Key]
        public int checkout_id { get; set; }
        public string product_name { get; set; }
        public int product_quantity { get; set; }
        public string std_email { get; set; }
        public int card_number { get; set; }
        public int order_date { get; set; }
        public decimal Cart_price { get; set; }
        public decimal Cart_total_price { get; set; }
        public int Cvv { get; set; }
    }
}
