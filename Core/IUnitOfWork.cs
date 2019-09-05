using System.Threading.Tasks;

namespace Biblioteca.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}