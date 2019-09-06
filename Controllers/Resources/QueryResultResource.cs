using System.Collections.Generic;

namespace Biblioteca.Controllers.Resources
{
    public class QueryResultResource<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items{ get; set; }
    }
}