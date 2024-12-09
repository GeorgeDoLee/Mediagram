using Mediagram.Models;
using Mediagram.Repositories.Mediagram.Repositories;

namespace Mediagram.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository Articles { get; }
        IRepository<SubArticle> SubArticles { get; }
        IRepository<Category> Categories { get; }
        IRepository<Publisher> Publishers { get; }

        Task Complete();
    }
}
