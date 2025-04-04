using DomainModel;
using InfoTrack.ServiceWrapper.GoogleApi;
using InfoTrack.ServiceWrapper.HtmlHelper;

namespace Services.UrlPosition
{
    public class UrlPosition : IUrlPosition
    {

        private readonly IGoogleApi _googleApi;
        private readonly IHtmlHelper _htmlHelper;
        private const int defaultNum = 100;

        public UrlPosition(IGoogleApi googleApi, IHtmlHelper htmlHelper)
        {
            _googleApi = googleApi;
            _htmlHelper = htmlHelper;
        }

        public async Task<string> CalculatePositionUrl(SearchUrlRequest request)
        {
            if (string.IsNullOrEmpty(request.SearchTerm) || string.IsNullOrEmpty(request.UrlToSearch))
            {
                throw new ArgumentException("Search term and URL to search cannot be null or empty.");
            }

            var googleSearchHtml = await _googleApi.SearchPageWithElements(request.SearchTerm, defaultNum);
            var nodes = _htmlHelper.GenerateHtmlNodes(googleSearchHtml);

            List<string> positionOfUrl = new List<string>();
            for (int i = 0; i < nodes?.Count(); i++)
            {
                if (_htmlHelper.GetInnerHtml(nodes.ElementAt(i)).Contains(request.UrlToSearch)) 
                {
                    positionOfUrl.Add((i + 1).ToString());
                }
            }

            if(positionOfUrl.Count() == 0)
            {
                positionOfUrl.Add("0");
            }

            var positionUrl = string.Join(",", positionOfUrl);

            return positionUrl;
        }
    }

}
