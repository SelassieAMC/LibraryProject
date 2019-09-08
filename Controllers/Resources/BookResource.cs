using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Core.Models;

namespace Biblioteca.Controllers.Resources
{
    public class BookResource : KeyValuePair
    {
        public BookResource()
        {
            this.Categories = new Collection<KeyValuePair>();
        }
        public KeyValuePair Author { get; set; }
        public ICollection<KeyValuePair> Categories { get; set; }
        [Required]
        public string ISBN { get; set; }
    }
}