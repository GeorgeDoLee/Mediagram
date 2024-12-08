using HtmlAgilityPack;

namespace Mediagram.Services.Scrapping
{
    public class ArticleScraper
    {
        public async Task<string?> ScrapeHeadlineAsync(string url)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch the article from URL: {url}");
            }

            var htmlContent = await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            var headline = htmlDocument.DocumentNode.SelectSingleNode("//title")?.InnerText;

            if (!string.IsNullOrEmpty(headline))
            {
                return headline.Trim();
            }

            var metaHeadline = htmlDocument.DocumentNode
                .SelectSingleNode("//meta[@property='og:title' or @name='twitter:title']")
                ?.GetAttributeValue("content", null);

            if (!string.IsNullOrEmpty(metaHeadline))
            {
                return metaHeadline.Trim();
            }

            return null;
        }
    }
}
