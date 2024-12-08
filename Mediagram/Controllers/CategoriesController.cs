using Mediagram.DTOs;
using Mediagram.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mediagram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();

            return response.Success ? Ok(response) : NotFound(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto dto)
        {
            var response = await _categoryService.AddCategoryAsync(dto);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto dto)
        {
            var response = await _categoryService.UpdateCategoryAsync(id, dto);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
