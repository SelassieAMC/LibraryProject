

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Core.Models
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            this.BookCategories = new Collection<BookCategory>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public DateTime? LastUpdate{ get; set; }
    }
}