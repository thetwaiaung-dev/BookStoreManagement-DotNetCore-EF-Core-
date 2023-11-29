using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
