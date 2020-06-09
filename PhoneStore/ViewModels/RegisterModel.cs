using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter company")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}