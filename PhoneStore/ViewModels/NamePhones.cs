

using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels
{
    public class NamePhones
    {
        [Required]
        public string NameOnePhone { get; set; }
        [Required]
        public string NameSecondPhone { get; set; }
    }
}