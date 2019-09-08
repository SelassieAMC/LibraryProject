using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Core.Models
{
    [Table("Books")]
    public class Book
    {
        public Book()
        {
            this.Categories = new Collection<BookCategory>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public ICollection<BookCategory> Categories { get; set; }
        public string ISBN { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}