using Biblioteca.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Book> Books { get; set;}

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        :base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<BookCategory>().HasKey(bc => new {
                bc.BookId, bc.CategoryId
            });
            //ISBN is an unique code asigned for every book
            modelBuilder.Entity<Book>().HasIndex( b => b.ISBN).IsUnique();
        }
    }
}