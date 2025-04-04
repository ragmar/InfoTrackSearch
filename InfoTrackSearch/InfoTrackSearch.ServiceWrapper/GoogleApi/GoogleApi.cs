using System.Web;

namespace InfoTrack.ServiceWrapper.GoogleApi
{
    public class GoogleApi : IGoogleApi
    {
        private const string googleUrl = "https://www.google.co.uk/search";
        private readonly HttpClient _httpClient;

        public GoogleApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SearchPageWithElements(string searchTerm, int num)
        {
            return await ReadGoogleSearchHtmlFileAsync("files/googlesearch.html");

            var htmlContent = string.Empty;
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["q"] = searchTerm;
            query["num"] = num.ToString();
            var requestUrl = $"{googleUrl}?{query}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            htmlContent = await response.Content.ReadAsStringAsync();

            return htmlContent;
        }

        private async Task<string> ReadGoogleSearchHtmlFileAsync(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
