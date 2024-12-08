using Mediagram.Models.Enums;

namespace Mediagram.DTOs
{
    public class PublisherDto
    {
        public required string Name { get; set; }
        public required PublisherBias Bias { get; set; }
    }
}
