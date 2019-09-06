using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Core;
using Biblioteca.Core.Models;
using Biblioteca.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Persistence
{
    public class AuthorRepository : IAuthorsRepository
    {
        private readonly LibraryDbContext context;
        public AuthorRepository(LibraryDbContext context)
        {
            this.context = context;
        }
        public void AddAuthorAsync(Author author)
        {
            context.Authors.Add(author);
        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await context.Authors.FindAsync(authorId);
        }

        public async Task<QueryResult<Author>> GetAuthorsAsync(GenericQuery query)
        {
            var result = new QueryResult<Author>();
            
            var dataQuery = context.Authors.OrderBy(A => A.Id).AsQueryable();
            dataQuery = dataQuery.ApplyPaging(query);
            
            result.TotalItems = await dataQuery.CountAsync();
            result.Items = await dataQuery.ToListAsync();
            return result;
        }
        public void RemoveAuthorAsync(Author author)
        {
            context.Remove(author);
        }
    }
}