using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Core.Models;

namespace Biblioteca.Core
{
    public interface IAuthorsRepository
    {
        void AddAuthorAsync(Author author);
        void RemoveAuthorAsync(Author author);
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task<QueryResult<Author>> GetAuthorsAsync(GenericQuery query);
    }
}