using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels
{
    public class InsertPhone
    {
        [Required(ErrorMessage = "Please enter phone name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter phone country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please enter phone price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please select company")]
        public string CompanyId { get; set; }
    }
}
