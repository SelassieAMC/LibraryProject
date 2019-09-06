using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Core;
using Biblioteca.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Persistence {
    public class BookRepository : IBookRepository {
        private readonly LibraryDbContext context;
        public BookRepository (LibraryDbContext context) {
            this.context = context;
        }
        public void AddBookAsync (Book book) {
            context.Books.Add(book);
        }

        public async Task<Book> GetBookByIdAsync (int bookId, bool includeRelatedObjects = true) {
            return await context.Books
            .Include(B => B.BookCategories)
            .Include(B => B.Author)
            .FirstOrDefaultAsync(B => B.Id.Equals(bookId));
        }

        public async Task<QueryResult<Book>> GetBooksAsync (BookQuery query) {
            var result = new QueryResult<Book>();
            
            var books =  context.Books
            .Include(B => B.BookCategories)
            .Include(B => B.Author).AsQueryable();

            return result;
        }

        public void RemoveBookAsync (Book book) {
            throw new System.NotImplementedException ();
        }
    }
}