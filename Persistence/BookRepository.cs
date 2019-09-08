using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Core;
using Biblioteca.Core.Models;
using Biblioteca.Extensions;
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
            if(includeRelatedObjects)
                return await context.Books
                    .Include(B => B.Categories)
                        .ThenInclude(bc => bc.Category)
                    .Include(B => B.Author)
                    .FirstOrDefaultAsync(B => B.Id.Equals(bookId));
            return await context.Books.FindAsync(bookId);
        }

        public async Task<QueryResult<Book>> GetBooksAsync (BookQuery queryObj) {
            var result = new QueryResult<Book>();
            
            var query =  context.Books.OrderBy(B => B.Id)
            .Include(B => B.Categories)
                .ThenInclude( bc => bc.Category)
            .Include(B => B.Author).AsQueryable();

            query = query.ApplyFiltering(queryObj);

            query = query.ApplyPaging(queryObj);

            result.TotalItems = await query.CountAsync();
            result.Items = await query.ToListAsync();
            return result;
        }

        public void RemoveBookAsync (Book book) {
            context.Books.Remove(book);
        }
    }
}