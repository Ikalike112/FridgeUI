using System.ComponentModel.DataAnnotations;

namespace FridgeUI.Models.Auth
{
    public class LoginModelDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
