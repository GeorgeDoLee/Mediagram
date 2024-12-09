namespace Mediagram.Models
{
    public class Article
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Photo {  get; set; }  
        public required DateTime PublishedDate { get; set; }
        public int TrendingScore { get; set; } = 0;
        public required bool IsBlindSpot { get; set; }
        public required float ProGovernmentCoverage { get; set; }
        public required float ProOppositionCoverage { get; set; }
        public required float CentristCoverage { get; set; }
        
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<SubArticle> SubArticles { get; set; } = new List<SubArticle>();
    }
}
