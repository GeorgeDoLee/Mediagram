using Mediagram.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediagram.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<SubArticle> SubArticles { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<CoveragePercentage> CoveragePercentages { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .HasMany(a => a.SubArticles)
                .WithOne(sa => sa.Article)
                .HasForeignKey(sa => sa.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CoveragePercentage>()
                .HasOne(cp => cp.Article)
                .WithOne(a => a.CoveragePercentage)
                .HasForeignKey<CoveragePercentage>(cp => cp.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubArticle>()
                .HasOne(sa => sa.Publisher)
                .WithMany(p => p.SubArticles)
                .HasForeignKey(sa => sa.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
