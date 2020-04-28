using System.Collections.Generic;
using PhoneStore.Models;

namespace PhoneStore.ViewModels
{
    public class IndexModel
    {
        public List<Phone> Phones { get; set; }
        public List<Company> Companies { get; set; }
    }
}