using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Core;
using Biblioteca.Core.Models;
using Biblioteca.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext context;
        public CategoryRepository(LibraryDbContext context)
        {
            this.context = context;
        }

        public void AddCategoryAsync(Category category)
        {
            context.Categories.Add(category);
        }
        public async Task<QueryResult<Category>> GetCategoriesAsync(GenericQuery query)
        {
            var result = new QueryResult<Category>();
            
            var dataQuery = context.Categories.OrderBy(C => C.Id).AsQueryable();
            dataQuery = dataQuery.ApplyPaging(query);
            
            result.TotalItems = await dataQuery.CountAsync();
            result.Items = await dataQuery.ToListAsync();
            return result;
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await context.Categories.FindAsync(categoryId);
        }
        public void RemoveCategoryAsync(Category category)
        {
            context.Remove(category);
        }
    }
}