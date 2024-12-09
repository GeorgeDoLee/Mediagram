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


        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            
            return categories;
        }


        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(id);

            category.TrendingScore++;
            await _unitOfWork.Complete();

            return category;
        }


        public async Task<Category> AddCategoryAsync(CategoryDto dto)
        {
            
            var category = new Category
            {
                Name = dto.Name,
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.Complete();

            return category;
        }


        public async Task<Category> UpdateCategoryAsync(int id, CategoryDto dto)
        {
            

            var existingCategory = await _unitOfWork.Categories.GetAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = dto.Name;

            await _unitOfWork.Complete();

            return existingCategory;
        }


        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(id);

            if (category == null)
            {
                return false;
            }

            await _unitOfWork.Categories.RemoveAsync(category);
            await _unitOfWork.Complete();

            return true;
        }
    }
}
