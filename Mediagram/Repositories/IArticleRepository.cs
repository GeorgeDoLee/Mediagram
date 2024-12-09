using Mediagram.Models;

namespace Mediagram.Repositories
{
    namespace Mediagram.Repositories
    {
        public interface IArticleRepository : IRepository<Article>
        {
            Task<IEnumerable<Article>> GetArticlesAsync(int pageNumber, int pageSize, bool? isBlindspot, int? categoryId);
        }
    }
}
