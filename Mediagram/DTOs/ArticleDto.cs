namespace Mediagram.DTOs
{
    public class ArticleDto
    {
        public required string Title { get; set; }
        public required int CategoryId { get; set; }
        public required Dictionary<int, string> ArticleUrls { get; set; } = new Dictionary<int, string>(); 
        // where kei is publisherId and value is url of article
    }
}
