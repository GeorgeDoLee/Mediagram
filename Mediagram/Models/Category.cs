namespace Mediagram.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int TrendingScore { get; set; } = 0;

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
