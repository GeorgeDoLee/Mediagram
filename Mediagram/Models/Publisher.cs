using Mediagram.Models.Enums;

namespace Mediagram.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Logo { get; set; }
        public required PublisherBias Bias { get; set; }

        public ICollection<SubArticle> SubArticles { get; set; } = new List<SubArticle>();
    }
}
