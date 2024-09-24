using System.ComponentModel.DataAnnotations;

namespace DecorVista.Models
{
    public class Designer
    {
        [Key]
        public int designer_id { get; set; }
        public string designer_first_name { get; set; }
        public string designer_last_name { get; set; }
        public string designer_contact { get; set; }
        public string designer_email { get; set; }
        public string designer_password { get; set; }
        public string designer_address { get; set; }
        public string designer_experience { get; set; }
        public string designer_specialization { get; set; }
        public string designer_portfolio { get; set; }
        public string designer_image { get; set; }
    }
}
