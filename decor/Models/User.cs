using System.ComponentModel.DataAnnotations;

namespace decor.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }

        [Required(ErrorMessage = "User name is required")]
        [StringLength(100, ErrorMessage = "User name cannot be longer than 100 characters")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string user_email { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid contact number")]
        public string user_contact { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
         

        public string user_password { get; set; }
        [Required]
        public string user_image { get; set; }
    }
}
