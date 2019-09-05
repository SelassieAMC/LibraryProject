using Biblioteca.Extensions;

namespace Biblioteca.Core.Models
{
    public class GenericQuery : IQueryObject
    {
        public int Page { get ; set; }
        public byte PageSize { get ; set; }
    }
}