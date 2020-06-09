using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}