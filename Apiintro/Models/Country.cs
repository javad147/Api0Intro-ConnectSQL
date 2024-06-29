using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apiintro.Models
{
    public class Country : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
