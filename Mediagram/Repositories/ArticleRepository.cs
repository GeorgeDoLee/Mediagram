using Mediagram.Models;
using Mediagram.Repositories.Mediagram.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mediagram.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext context) : base(context)
        {
        }

        public override async Task<Article> GetAsync(int id)
        {
            return await _context.Set<Article>()
                .Include(a => a.Category)
                .Include(a => a.SubArticles)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync(int pageNumber, int pageSize, bool? isBlindspot, int? categoryId)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than or equal to 1.", nameof(pageNumber));
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than or equal to 1.", nameof(pageSize));

            var query = _context.Set<Article>()
                .Include(a => a.Category)
                .AsQueryable();

            if (isBlindspot.HasValue)
            {
                query = query.Where(a => a.IsBlindSpot == isBlindspot.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == categoryId.Value);
            }

            return await query
                .OrderByDescending(a => a.PublishedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
