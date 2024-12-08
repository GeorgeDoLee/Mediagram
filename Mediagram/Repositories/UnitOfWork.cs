using Mediagram.Data;
using Mediagram.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediagram.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public IRepository<Article> Articles { get; }
        public IRepository<SubArticle> SubArticles { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<Publisher> Publishers { get; }
        public IRepository<CoveragePercentage> CoveragePercentages { get; }

        public UnitOfWork(MediagramContext context)
        {
            _context = context;

            Articles = new Repository<Article>(context);
            SubArticles = new Repository<SubArticle>(context);
            Categories = new Repository<Category>(context);
            Publishers = new Repository<Publisher>(context);
            CoveragePercentages = new Repository<CoveragePercentage>(context);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}