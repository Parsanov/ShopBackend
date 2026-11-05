using Core.DTO.CategoriesDTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [HttpGet("api/categoris")]
        public async Task<ActionResult<List<CategoriesDTO>>> GetCategories()
        {
            var result = await _categoryRepository.GetAsync();

            return Ok(result);
        }
    }
}
