using Mediagram.Models;

namespace Mediagram.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> Articles { get; }
        IRepository<SubArticle> SubArticles { get; }
        IRepository<Categories> Categories { get; }
        IRepository<Publisher> Publishers { get; }
        IRepository<CoveragePercentage> CoveragePercentages { get; }

        void Complete();
    }
}
