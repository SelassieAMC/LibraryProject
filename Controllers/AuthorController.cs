using System;
using System.Threading.Tasks;
using AutoMapper;
using Biblioteca.Controllers.Resources;
using Biblioteca.Core;
using Biblioteca.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [Route("/api/authors/")]
    public class AuthorController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAuthorsRepository authorsRepository;
        private readonly IUnitOfWork unitOfWork;
        ///Constructor con los servicios inyectados
        ///mapper = mapeo de objetos automatico, repositorio = desacoplamiento de ORM, 
        ///unitOfWork division de instancias para almacenado en BD
        public AuthorController(IMapper mapper, IAuthorsRepository authorsRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.authorsRepository = authorsRepository;
            this.mapper = mapper;
        }
        ///Metodo post para agregar un autor a base de datos
        ///recibe un objecto authorResource en formato JSON
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorResource authorResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var author = mapper.Map<AuthorResource, Author>(authorResource);
                author.LastUpdate = DateTime.Now;
                authorsRepository.AddAuthorAsync(author);
                await unitOfWork.CompleteAsync();

                var newAuthor = await authorsRepository.GetAuthorByIdAsync(author.Id);

                return Ok(mapper.Map<Author,AuthorResource>(newAuthor));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo put para actualizar un autor en base de datos, 
        ///Recibe el id del autor y un objecto authorResource en formato JSON
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpPut("update/{authorId}")]
        public async Task<IActionResult> UpdateAuthor(int authorId, [FromBody]AuthorResource authorResource){
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var author = await authorsRepository.GetAuthorByIdAsync(authorId);
                if(author == null)
                    return NotFound();

                mapper.Map<AuthorResource,Author>(authorResource,author);
                author.LastUpdate = DateTime.Now;

                await unitOfWork.CompleteAsync();
                author = await authorsRepository.GetAuthorByIdAsync(author.Id);
                var result = mapper.Map<Author,AuthorResource>(author);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo delete para eliminar un autor en base de datos
        ///Recibe el id del autor
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveAuthorByIdAsync(int id){
            try
            {
                var author = await authorsRepository.GetAuthorByIdAsync(id);
                if(author==null)
                    return NotFound();

                authorsRepository.RemoveAuthorAsync(author);
                await unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo get para obtener los autores de base de datos de acuerdo a paginado y cantidad de registros
        ///Recibe GenericQuery con pagina  y cantidad a devolver
        ///Devuelve lista de autores que cumplan las condiciones de GenericQuery
        [HttpGet]
        public async Task<IActionResult> GetAuthors(GenericQuery query){
            try
            {
                var authors = await authorsRepository.GetAuthorsAsync(query);
                return Ok (mapper.Map<QueryResult<Author>,QueryResultResource<AuthorResource>>(authors));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///Metodo get para obtener autor por id
        ///Recibe entero con el id del autor
        ///Devuelve resultado del proceso de busqueda
        [HttpGet("getauthor/{id}")]
        public async Task<IActionResult> GetAuthorById(int id){
            try
            {
                var author = await authorsRepository.GetAuthorByIdAsync(id);
                if(author==null)
                    return NotFound();
                return Ok (mapper.Map<Author,AuthorResource>(author));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}