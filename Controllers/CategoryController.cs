using System;
using System.Threading.Tasks;
using AutoMapper;
using Biblioteca.Controllers.Resources;
using Biblioteca.Core;
using Biblioteca.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [Route("/api/categories/")]
    public class CategoryController: Controller
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoriesRepository;
        private readonly IUnitOfWork unitOfWork;
        ///Constructor con los servicios inyectados
        ///mapper = mapeo de objetos automatico, repositorio = desacoplamiento de ORM, 
        ///unitOfWork division de instancias para almacenado en BD
        public CategoryController(IMapper mapper, ICategoryRepository categoriesRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.categoriesRepository = categoriesRepository;
            this.mapper = mapper;
        }
        ///Metodo post para agregar una categoria a base de datos
        ///recibe un objecto categoryResource en formato JSON
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryResource categoryResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var category = mapper.Map<CategoryResource, Category>(categoryResource);
                category.LastUpdate = DateTime.Now;
                categoriesRepository.AddCategoryAsync(category);
                await unitOfWork.CompleteAsync();

                var newCategory = await categoriesRepository.GetCategoryByIdAsync(category.Id);

                return Ok(mapper.Map<Category,CategoryResource>(newCategory));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo put para actualizar una categoria en base de datos, 
        ///Recibe el id de la categoria y un objecto categoryResource en formato JSON
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpPut("update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody]CategoryResource categoryResource){
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var category = await categoriesRepository.GetCategoryByIdAsync(categoryId);
                if(category == null)
                    return NotFound();

                mapper.Map<CategoryResource,Category>(categoryResource,category);
                category.LastUpdate = DateTime.Now;

                await unitOfWork.CompleteAsync();
                category = await categoriesRepository.GetCategoryByIdAsync(category.Id);
                var result = mapper.Map<Category,CategoryResource>(category);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo delete para eliminar una categoria en base de datos
        ///Recibe el id de la categoria
        ///Devuelve resultado con respuesta del proceso ejecutado
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveCategoryByIdAsync(int id){
            try
            {
                var category = await categoriesRepository.GetCategoryByIdAsync(id);
                if(category==null)
                    return NotFound();

                categoriesRepository.RemoveCategoryAsync(category);
                await unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo get para obtener las categorias de base de datos de acuerdo a paginado y cantidad de registros
        ///Recibe GenericQuery con pagina  y cantidad a devolver
        ///Devuelve lista de categorias que cumplan las condiciones de GenericQuery
        [HttpGet]
        public async Task<IActionResult> GetCategories(GenericQuery query){
            try
            {
                var categories = await categoriesRepository.GetCategoriesAsync(query);
                return Ok(mapper.Map<QueryResult<Category>,QueryResultResource<CategoryResource>>(categories));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///Metodo get para obtener categoria por id
        ///Recibe entero con el id de la categoria
        ///Devuelve resultado del proceso de busqueda
        [HttpGet("getcategory/{id}")]
        public async Task<IActionResult> GetCategoryById(int id){
            try
            {
                var category = await categoriesRepository.GetCategoryByIdAsync(id);
                if(category==null)
                    return NotFound();
                return Ok (mapper.Map<Category,CategoryResource>(category));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}