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

        ///Metodo post para agregar una categoria a base de datos
        ///recibe un objecto categoryResource en formato JSON
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

                return Ok(mapper.Map<Book,BookResource>(newBook));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}