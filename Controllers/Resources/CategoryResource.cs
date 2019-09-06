using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Controllers.Resources
{
    public class CategoryResource : KeyValuePair
    {
        [StringLength(255)]
        public string Description { get; set; }
    }
}