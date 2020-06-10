using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.ViewModels
{
    public class DeletePhone
    {
        [Required(ErrorMessage = "Please enter phone name")]
        public string Name { get; set; }
    }
}
