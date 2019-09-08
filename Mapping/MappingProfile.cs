using System.Linq;
using AutoMapper;
using Biblioteca.Controllers.Resources;
using Biblioteca.Core.Models;

namespace Biblioteca.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            //Domain model to API Resource
            CreateMap<Author, AuthorResource>();
            CreateMap<Category,CategoryResource>();
            CreateMap<QueryResult<Category>,QueryResultResource<CategoryResource>>();
            CreateMap<QueryResult<Author>,QueryResultResource<AuthorResource>>();
            CreateMap<QueryResult<Book>,QueryResultResource<BookResource>>();
            CreateMap<Book, BookResource>()
                .ForMember(br => br.Categories, opt => 
                    opt.MapFrom (b => b.Categories
                    .Select(bc => new KeyValuePair { Id = bc.Category.Id, Name = bc.Category.Name})))
                .ForMember(br => br.Author, opt => 
                    opt.MapFrom (b => new KeyValuePair { Id = b.Author.Id, Name = b.Author.Name}));
            CreateMap<BookCategory, BookCategoryResource>();
            //API Resource to Domain Model
            CreateMap<AuthorResource,Author>();
            CreateMap<CategoryResource,Category>();
            CreateMap<SaveBookResource, Book>()
                .ForMember(b => b.Id, opt => opt.Ignore())
                .ForMember(b => b.AuthorId, opt => opt.MapFrom(sbr => sbr.AuthorId))
                .ForMember(b => b.Categories, opt => opt.Ignore())
                .AfterMap((bsr, b) => {
                    var removedCategories = b.Categories.Where( c => !bsr.Categories.Contains(c.CategoryId)).ToList();
                    foreach(var c in removedCategories){
                        b.Categories.Remove(c);
                    }
                    var addedCategories = bsr.Categories
                        .Where(id => !b.Categories.Any(c => c.CategoryId.Equals(id)))
                        .Select(id => new BookCategory { CategoryId = id }).ToList();
                    foreach (var c in addedCategories)
                    {
                        b.Categories.Add(c);
                    }
                });
        }
    }
}