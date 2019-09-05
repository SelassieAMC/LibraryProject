using Biblioteca.Extensions;

namespace Biblioteca.Core
{
    public class BookQuery : IQueryObject
    {
        public string BookName { get; set;}
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public int Page { get ; set; }
        public byte PageSize { get ; set; }
    }
}