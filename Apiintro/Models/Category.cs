using System.ComponentModel.DataAnnotations;

namespace Apiintro.Models
{
    public class Category: BaseEntity
    {
        [Required (ErrorMessage ="Bosh buraxma")]
        public string Name { get; set; }
    }
}
