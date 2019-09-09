using Microsoft.AspNetCore.Mvc;
using Biblioteca.Core;
using AutoMapper;
using System.Threading.Tasks;
using Biblioteca.Controllers.Resources;
using Biblioteca.Core.Models;
using System;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Controllers
{
    [Route("/api/books/")]
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private ILogger<BookController> _logger;
        ///Constructor con los servicios inyectados
        ///mapper = mapeo de objetos automatico, repositorio = desacoplamiento de ORM, 
        ///unitOfWork division de instancias para almacenado en BD
        public BookController(IMapper mapper,
         IBookRepository bookRepository, 
         IUnitOfWork unitOfWork,
         ILogger<BookController> logger)
        {
            this.unitOfWork = unitOfWork;
            this._logger = logger;
            this.mapper = mapper;
            this.bookRepository = bookRepository;
        }

        public ILogger<BookController> Logger { get; }

        ///Metodo post para agregar un libro a base de datos
        ///recibe un objecto SaveBookResource 
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] SaveBookResource sbResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var book = mapper.Map<SaveBookResource, Book>(sbResource);
                book.LastUpdate = DateTime.Now;
                bookRepository.AddBookAsync(book);
                await unitOfWork.CompleteAsync();

                var newBook = await bookRepository.GetBookByIdAsync(book.Id);
                _logger.LogInformation(new EventId(),null,newBook,null);
                return Ok(mapper.Map<Book,BookResource>(newBook));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo put para actualizar un libro en base de datos, 
        ///Recibe el id del libro y un objecto SaveBookResource 
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpPut("update/{bookId}")]
        public async Task<IActionResult> UpdateAuthor(int bookId, [FromBody]SaveBookResource sbResource){
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var book = await bookRepository.GetBookByIdAsync(bookId);
                if(book == null)
                    return NotFound();

                mapper.Map<SaveBookResource,Book>(sbResource,book);
                book.LastUpdate = DateTime.Now;

                await unitOfWork.CompleteAsync();
                book = await bookRepository.GetBookByIdAsync(book.Id);
                var result = mapper.Map<Book,BookResource>(book);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo delete para eliminar un libro en base de datos
        ///Recibe el id del libro
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveBookByIdAsync(int id){
            try
            {
                var book = await bookRepository.GetBookByIdAsync(id,false);
                if(book==null)
                    return NotFound();

                bookRepository.RemoveBookAsync(book);
                await unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo get para obtener los libros de acuerdo a paginado,cantidad, nombre, categorias o autor
        ///Recibe objeto BookQuery 
        ///Devuelve lista de libros que cumplan las condiciones de BookQuery
        [HttpGet]
        public async Task<IActionResult> GetBooks(BookQuery query){
            try
            {
                var books = await bookRepository.GetBooksAsync(query);
                return Ok (mapper.Map<QueryResult<Book>,QueryResultResource<BookResource>>(books));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo get para obtener libro por id
        ///Recibe entero con el id del libro
        ///Devuelve resultado del proceso de busqueda
        [HttpGet("getbook/{id}")]
        public async Task<IActionResult> GetAuthorById(int id){
            try
            {
                var book = await bookRepository.GetBookByIdAsync(id);
                if(book==null)
                    return NotFound();
                return Ok (mapper.Map<Book,BookResource>(book));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}