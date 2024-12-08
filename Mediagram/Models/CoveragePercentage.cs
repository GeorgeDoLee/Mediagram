namespace Mediagram.Models
{
    public class CoveragePercentage
    {
        public int Id { get; set; }
        public required float ProGovernmentCoverage { get; set; }
        public required float ProOppositionCoverage { get; set; }
        public required float CentristCoverage { get; set; }

        public int? ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
