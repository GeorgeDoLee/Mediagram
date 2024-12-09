using Microsoft.EntityFrameworkCore;
using Mediagram.Models;

namespace Mediagram.Data
{
    public class MediagramContext : DbContext
    {
        public MediagramContext(DbContextOptions<MediagramContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubArticle> SubArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany()
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Entity<SubArticle>()
                .HasOne<Article>()
                .WithMany(a => a.SubArticles)
                .HasForeignKey(sa => sa.ArticleId);

            modelBuilder.Entity<SubArticle>()
                .HasOne(sa => sa.Publisher)
                .WithMany()
                .HasForeignKey(sa => sa.PublisherId);
        }
    }
}
