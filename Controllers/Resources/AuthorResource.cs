using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Controllers.Resources
{
    public class AuthorResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}