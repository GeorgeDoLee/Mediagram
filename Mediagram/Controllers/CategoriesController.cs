using Mediagram.Common;
using Mediagram.DTOs;
using Mediagram.Models;
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
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound(new ApiResponse("No categories found."));
            }

            return Ok(new ApiResponse(true, "Categories retrieved successfully.", categories));

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound(new ApiResponse("Category not found."));
            }

            return Ok(new ApiResponse(true, "Category retrieved successfully", category));
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse("Invalid category data."));
            }

            var category = await _categoryService.AddCategoryAsync(dto);

            return Ok(new ApiResponse(true, "Category added successfully.", category));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse("Category data is invalid."));
            }

            var category = await _categoryService.UpdateCategoryAsync(id, dto);

            if (category == null)
            {
                return NotFound(new ApiResponse("Category not found,"));
            }

            return Ok(new ApiResponse(true, "Category updated successfully.", category));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryDeleted = await _categoryService.DeleteCategoryAsync(id);

            return categoryDeleted ? 
                Ok(new ApiResponse(true, "Category deleted successfully.")) 
                : 
                NotFound(new ApiResponse("Category not found."));
        }
    }
}
