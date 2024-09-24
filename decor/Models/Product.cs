using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecorVista.Models
{
    public class Product
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_brand { get; set; }
        public decimal product_price { get; set; }
        public string product_dimensions { get; set; }
        public string product_materials { get; set; }
        public string product_description { get; set; }
        public string product_image { get; set; }

        [ForeignKey("Category")]
        public int category_id { get; set; }
        public Category Category { get; set; }

    }
}
