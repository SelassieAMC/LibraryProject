using Biblioteca.Extensions;

namespace Biblioteca.Core
{
    public class BookQuery : IQueryObject
    {
        public int? BookId { get; set;}
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int Page { get ; set; }
        public byte PageSize { get ; set; }
    }
}