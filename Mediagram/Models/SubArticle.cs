namespace Mediagram.Models
{
    public class SubArticle
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string SourceUrl { get; set; }

        public required int ArticleId { get; set; }
        public Article? Article { get; set; }

        public required int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
