namespace Mediagram.Models
{
    public class Article
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? Photo {  get; set; }  
        public required DateTime PublishedDate { get; set; }

        public ICollection<SubArticle> SubArticles { get; set; } = new List<SubArticle>();
        public CoveragePercentage? CoveragePercentage { get; set; }
    }
}
