using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Controllers.Resources
{
    public class KeyValuePair
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}