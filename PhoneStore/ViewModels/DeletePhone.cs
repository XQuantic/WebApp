using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels
{
    public class DeletePhone
    {
        [Required(ErrorMessage = "Please enter phone name")]
        public string Name { get; set; }
    }
}
