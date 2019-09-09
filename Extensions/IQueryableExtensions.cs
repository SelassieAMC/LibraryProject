using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using Biblioteca.Core;
using Biblioteca.Core.Models;

namespace Biblioteca.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Book> ApplyFiltering(this IQueryable<Book> query, BookQuery queryObj)
        {
            if (queryObj.BookName!=null)
                query = query.Where(b => b.Name == queryObj.BookName);

            if (queryObj.AuthorName!=null)
                query = query.Where(b => b.Author.Name == queryObj.AuthorName);

            if(queryObj.CategoryName!=null)
                query = query.Where(b => b.Categories.Any(bc => bc.Category.Name == queryObj.CategoryName));

            
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