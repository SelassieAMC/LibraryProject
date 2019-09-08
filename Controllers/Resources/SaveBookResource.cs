using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Core.Models;

namespace Biblioteca.Controllers.Resources
{
    public class SaveBookResource : KeyValuePair
    {
        public int AuthorId { get; set; }
        public ICollection<int> Categories { get; set; }
        [Required]
        public string ISBN { get; set; }
    }
}