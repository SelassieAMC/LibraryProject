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
            
            //API Resource to Domain Model
            CreateMap<AuthorResource,Author>();
            CreateMap<CategoryResource,Category>();
        }
    }
}