using Mediagram.Common;
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

            article.TrendingScore++;
            article.Category.TrendingScore++;
            await _unitOfWork.Complete();

            return article;
        }


        public async Task<Article> AddArticleAsync(ArticleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || dto.CategoryId <= 0)
            {
                throw new ArgumentException(ErrorMessages.InvalidData);
            }

            var (proGov, proOpp, centrist) = coverageCalculator(dto.ArticleUrls.Keys.ToList());
            bool isBlindSpot = IsBlindSpot(proGov, proOpp);

            var article = new Article
            {
                Title = dto.Title,
                PublishedDate = DateTime.UtcNow,
                CategoryId = dto.CategoryId,
                IsBlindSpot = isBlindSpot,
                ProGovernmentCoverage = proGov,
                ProOppositionCoverage = proOpp,
                CentristCoverage = centrist,
            };

            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.Complete();

            List<SubArticle> subArticles = new List<SubArticle>();

            foreach (var kvp in dto.ArticleUrls)
            {
                var publisherId = kvp.Key;
                var articleUrl = kvp.Value;

                var title = await _articleScraper.ScrapeHeadlineAsync(articleUrl);

                var subArticle = new SubArticle
                {
                    Title = title,
                    SourceUrl = articleUrl,
                    PublisherId = publisherId,
                    ArticleId = article.Id,
                };

                subArticles.Add(subArticle);
            }

            await _unitOfWork.SubArticles.AddRangeAsync(subArticles);
            await _unitOfWork.Complete();

            return article;
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


        private (float proGov, float proOpp, float centrist) coverageCalculator(List<int> publisherIds)
        {
            var totalSubArticles = publisherIds.Count;
            if (totalSubArticles == 0) return (0, 0, 0);

            int govCount = 0;
            int oppCount = 0;
            int centristCount = 0;

            foreach (var publisherId in publisherIds)
            {
                var publisher = _unitOfWork.Publishers.GetAsync(publisherId).Result;

                if (publisher?.Bias == PublisherBias.ProGovernment)
                    govCount++;
                else if (publisher?.Bias == PublisherBias.ProOpposition)
                    oppCount++;
                else
                    centristCount++;
            }

            return (
                proGov: (float)govCount / totalSubArticles * 100,
                proOpp: (float)oppCount / totalSubArticles * 100,
                centrist: (float)centristCount / totalSubArticles * 100
            );
        }

        private bool IsBlindSpot(float proGov, float proOpp)
        {
            return proGov >= 70 || proOpp >= 70;
        }
    }
}
