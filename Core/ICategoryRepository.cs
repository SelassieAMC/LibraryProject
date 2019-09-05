using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Core.Models;

namespace Biblioteca.Core
{
    public interface ICategoryRepository
    {
        void AddCategoryAsync(Category category);
        void RemoveCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int categoryId, bool includeRelatedObjects = true);
        Task<IEnumerable<Category>> GetCategoriesAsync(GenericQuery query);
    }
}