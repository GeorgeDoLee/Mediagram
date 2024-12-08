using Mediagram.Common;
using Mediagram.DTOs;
using Mediagram.Models;
using Mediagram.Repositories;

namespace Mediagram.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ApiResponse> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            if (categories == null || !categories.Any())
            {
                return new ApiResponse("No categories found.");
            }

            return new ApiResponse(true, "Categories retrieved successfully.", categories);
        }


        public async Task<ApiResponse> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(id);

            if (category == null)
            {
                return new ApiResponse("Category not found.");
            }

            return new ApiResponse(true, "Category retrieved successfully", category);
        }


        public async Task<ApiResponse> AddCategoryAsync(CategoryDto dto)
        {
            if (dto == null)
            {
                return new ApiResponse("Invalid category data.");
            }

            var category = new Category
            {
                Name = dto.Name,
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.Complete();

            return new ApiResponse(true, "Category added successfully.", category);
        }


        public async Task<ApiResponse> UpdateCategoryAsync(int id, CategoryDto dto)
        {
            if (dto == null)
            {
                return new ApiResponse("Category data is invalid.");
            }

            var existingCategory = await _unitOfWork.Categories.GetAsync(id);

            if (existingCategory == null)
            {
                return new ApiResponse("Category not found.");
            }

            existingCategory.Name = dto.Name;

            await _unitOfWork.Complete();

            return new ApiResponse(true, "Category updated successfully.", existingCategory);
        }


        public async Task<ApiResponse> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(id);

            if (category == null)
            {
                return new ApiResponse("Category not found.");
            }

            await _unitOfWork.Categories.RemoveAsync(category);
            await _unitOfWork.Complete();

            return new ApiResponse(true, "Category deleted successfully.");
        }
    }
}
