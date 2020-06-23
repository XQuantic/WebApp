

using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels
{
    public class NamePhones
    {
        [Required(ErrorMessage = "Please fill first phone")]
        public string NameOnePhone { get; set; }
        [Required(ErrorMessage = "Please fill second phone")]
        public string NameSecondPhone { get; set; }
    }
}