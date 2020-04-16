using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneStore.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Phone> Users { get; set; } 
    }
}