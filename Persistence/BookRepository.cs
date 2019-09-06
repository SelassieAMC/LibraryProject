using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Core;
using Biblioteca.Core.Models;

namespace Biblioteca.Persistence
{
    public class BookRepository : IBookRepository
    {
        public void AddBookAsync(Book book)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> GetBookByIdAsync(int bookId, bool includeRelatedObjects = true)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooksAsync(BookQuery query)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveBookAsync(Book book)
        {
            throw new System.NotImplementedException();
        }
    }
}