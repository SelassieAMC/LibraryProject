using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Controllers.Resources
{
    public class CategoryResource
    {
        public CategoryResource()
        {
            this.BookCategories = new Collection<BookCategoryResource>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public ICollection<BookCategoryResource> BookCategories { get; set; }
        public DateTime LastUpdate{ get; set; }
    }
}