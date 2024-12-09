using Mediagram.DTOs;
using Mediagram.Models;
using Mediagram.Models.Enums;
using Mediagram.Repositories;
using Mediagram.Services.Scrapping;

namespace Mediagram.Services
{
    public class ArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ArticleScraper _articleScraper = new ArticleScraper();
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync(int pageNumber, int pageSize, bool? isBlindSpot, int? categoryId)
        {
            var articles = await _unitOfWork.Articles.GetArticlesAsync(pageNumber, pageSize, isBlindSpot, categoryId);

            return articles;
        }


        public async Task<Article> GetArticleByIdAsync(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(id);

            return article;
        }


        public async Task<Article> AddArticleAsync(ArticleDto dto) // not implemented yet
        {
            throw new NotImplementedException();
        }


        public async Task<Article> UpdateArticleAsync(int id, ArticleDto dto) // not implemented yet
        {
            throw new NotImplementedException();
        }


        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(id);

            if (article == null)
            {
                return false;
            }

            await _unitOfWork.Articles.RemoveAsync(article);
            await _unitOfWork.Complete();

            return true;
        }
    }
}
