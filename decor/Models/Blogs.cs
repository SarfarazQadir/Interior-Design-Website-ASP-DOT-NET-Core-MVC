using System.ComponentModel.DataAnnotations;

namespace DecorVista.Models
{
    public class Blogs
    {
        [Key]
        public int blog_id { get; set; }
        public string blog_article_name { get; set; }
        public string blog_article_discription { get; set; }
        public string blog_article_image { get; set; }

    }
}

