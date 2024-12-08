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

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync();

            return articles;
        }


        public async Task<Article> GetArticleByIdAsync(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(id);

            return article;
        }


        private async Task<CoveragePercentage> CalculateCoveragePercentageAsync(List<Publisher> publishers)
        {
            int totalPublishers = publishers.Count;

            int proGovernmentCount = publishers.Count(p => p.Bias == PublisherBias.ProGovernment);
            int proOppositionCount = publishers.Count(p => p.Bias == PublisherBias.ProOpposition);
            int centristCount = publishers.Count(p => p.Bias == PublisherBias.Centrist);

            float proGovernmentPercentage = (float)proGovernmentCount / totalPublishers * 100;
            float proOppositionPercentage = (float)proOppositionCount / totalPublishers * 100;
            float centristPercentage = (float)centristCount / totalPublishers * 100;

            return new CoveragePercentage
            {
                ProGovernmentCoverage = proGovernmentPercentage,
                ProOppositionCoverage = proOppositionPercentage,
                CentristCoverage = centristPercentage
            };
        }


        private bool IsBlindSpot(CoveragePercentage coveragePercentage)
        {
            const float Threshold = 70.0f;

            return coveragePercentage.ProGovernmentCoverage >= Threshold ||
                   coveragePercentage.ProOppositionCoverage >= Threshold;
        }


        public async Task<Article> AddArticleAsync(ArticleDto dto)
        {
            var subArticles = new List<SubArticle>();

            var publisherIds = dto.ArticleUrls.Keys.ToList();
            var publishers = await _unitOfWork.Publishers.FindAsync(p => publisherIds.Contains(p.Id));

            foreach (var publisher in publishers)
            {
                var url = dto.ArticleUrls[publisher.Id];

                var scrapedHeadline = await _articleScraper.ScrapeHeadlineAsync(url);

                var subArticle = new SubArticle
                {
                    Title = scrapedHeadline,
                    SourceUrl = url,
                    PublisherId = publisher.Id
                };
                subArticles.Add(subArticle);
            }

            var coveragePercentage = await CalculateCoveragePercentageAsync(publishers.ToList());

            var isBlindSpot = IsBlindSpot(coveragePercentage);

            var article = new Article
            {
                Title = dto.Title,
                PublishedDate = DateTime.UtcNow,
                CategoryId = dto.CategoryId,
                IsBlindSpot = isBlindSpot
            };

            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.Complete();

            coveragePercentage.ArticleId = article.Id;
            await _unitOfWork.CoveragePercentages.AddAsync(coveragePercentage);

            foreach (var subArticle in subArticles)
            {
                subArticle.ArticleId = article.Id;
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
    }
}
