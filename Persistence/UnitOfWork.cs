using System.Threading.Tasks;
using Biblioteca.Core;

namespace Biblioteca.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext context;
        public UnitOfWork(LibraryDbContext context)
        {
            this.context = context;

        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}