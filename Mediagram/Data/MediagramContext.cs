using Microsoft.EntityFrameworkCore;
using Mediagram.Models;

namespace Mediagram.Data
{
    public class MediagramContext : DbContext
    {
        public MediagramContext(DbContextOptions<MediagramContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoveragePercentage> CoveragePercentages { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubArticle> SubArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.CoveragePercentage)
                .WithOne(c => c.Article)
                .HasForeignKey<CoveragePercentage>(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubArticle>()
                .HasOne(sa => sa.Article)
                .WithMany(a => a.SubArticles)
                .HasForeignKey(sa => sa.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubArticle>()
                .HasOne(sa => sa.Publisher)
                .WithMany(p => p.SubArticles)
                .HasForeignKey(sa => sa.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
