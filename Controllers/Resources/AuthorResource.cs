using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Controllers.Resources
{
    public class AuthorResource : KeyValuePair
    {
        [StringLength(255)]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}