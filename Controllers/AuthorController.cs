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
        public AuthorController(IMapper mapper, IAuthorsRepository authorsRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.authorsRepository = authorsRepository;
            this.mapper = mapper;

        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorResource authorResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var author = mapper.Map<AuthorResource, Author>(authorResource);
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
        [HttpPut("/update/{id}")]
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
        [HttpPost("/remove/{id}")]
        public async Task<IActionResult> RemoveAuthor(int id){
            try
            {
                var author = await authorsRepository.GetAuthorByIdAsync(id);
                if(author==null)
                    return NotFound();

                authorsRepository.RemoveAuthorAsync(author);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}