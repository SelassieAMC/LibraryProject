using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Core.Models
{
    [Table("Authors")]
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}