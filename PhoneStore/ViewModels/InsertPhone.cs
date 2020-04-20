using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels
{
    public class InsertPhone
    {
        [Required]
        public string NamePhone { get; set; }
        [Required]
        public string CountryPhone { get; set; }
        [Required]
        public double PricePhone { get; set; }
        [Required]
        public int CompanyPhone { get; set; }
    }
}