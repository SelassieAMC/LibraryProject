using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Core.Models;

namespace Biblioteca.Controllers.Resources
{
    public class BookResource
    {
        public BookResource()
        {
            this.BookCategories = new Collection<BookCategoryResource>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public ICollection<BookCategoryResource> BookCategories { get; set; }
        public string ISBN { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}