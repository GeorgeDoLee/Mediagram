using Mediagram.Common;
using Mediagram.DTOs;
using Mediagram.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mediagram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _articleService;
        public ArticlesController(ArticleService articleService)
        {
            _articleService = articleService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllArticles(int pageNumber, int pageSize, bool? isBlindSpot, int? categoryId)
        {
            var articles = await _articleService.GetAllArticlesAsync(pageNumber, pageSize, isBlindSpot, categoryId);

            if (articles == null || !articles.Any())
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Articles retrieved successfully.", articles));

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Article retrieved successfully.", article));
        }


        [HttpPost]
        public async Task<IActionResult> AddArticle(ArticleDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse(ErrorMessages.InvalidData));
            }

            var article = await _articleService.AddArticleAsync(dto);

            return Ok(new ApiResponse(true, "Article added successfully.", article));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, ArticleDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse(ErrorMessages.InvalidData));
            }

            var article = await _articleService.UpdateArticleAsync(id, dto);

            if (article == null)
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Article updated successfully.", article));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var categoryDeleted = await _articleService.DeleteArticleAsync(id);

            return categoryDeleted ?
                Ok(new ApiResponse(true, "Article deleted successfully."))
                :
                NotFound(new ApiResponse(ErrorMessages.NotFound));
        }


        [HttpPost("upload-photo/{id}")]
        public async Task<IActionResult> UploadArticlePhoto(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new ApiResponse(ErrorMessages.InvalidData));
            }

            var article = await _articleService.UploadArticlePhotoAsync(id, file);

            if (article == null)
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Photo uploaded successfully.", article));
        }


        [HttpDelete("delete-photo/{id}")]
        public async Task<IActionResult> DeleteArticlePhoto(int id)
        {
            var photoDeleted = await _articleService.DeleteArticlePhotoAsync(id);

            return photoDeleted ?
                Ok(new ApiResponse(true, "Photo deleted successfully."))
                :
                NotFound(new ApiResponse(ErrorMessages.NotFound));
        }
    }
}
