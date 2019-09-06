using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Core.Models;

namespace Biblioteca.Core
{
    public interface IBookRepository
    {
        void AddBookAsync(Book book);
        void RemoveBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int bookId, bool includeRelatedObjects = true);
        Task<QueryResult<Book>> GetBooksAsync(BookQuery query);
    }
}