using System.Linq;
using Biblioteca.Core;
using Biblioteca.Core.Models;

namespace Biblioteca.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Book> ApplyFiltering(this IQueryable<Book> query, BookQuery queryObj)
        {
            if (queryObj.BookId.HasValue)
                query = query.Where(b => b.Id == queryObj.BookId.Value);

            if (queryObj.AuthorId.HasValue)
                query = query.Where(b => b.AuthorId == queryObj.AuthorId.Value);

                //falto por categoria

            return query; 
        }
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj){
            if(queryObj.Page <= 0)
                queryObj.Page = 1;

            if(queryObj.PageSize <= 0)
                queryObj.PageSize = 10;

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}