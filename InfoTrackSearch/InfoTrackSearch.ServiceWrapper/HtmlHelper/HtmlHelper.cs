using HtmlAgilityPack;

namespace InfoTrack.ServiceWrapper.HtmlHelper
{
    public class HtmlHelper : IHtmlHelper
    {
        private const string _spaceCharacter = "%20";
        private HtmlDocument _htmlDoc;
        public HtmlNodeCollection GenerateHtmlNodes(string html)
        {
            _htmlDoc = new HtmlDocument();
            _htmlDoc.LoadHtml(html);
            var searchNodes = GetNodeByXPath($"//div[contains(@data-async-context, 'query:')]");
            var classOfChild = GetAttributeValue(GetChild(searchNodes, 0), "class");
            return GetNodesByXPath($"//div[@class=\"{classOfChild}\"]");
        }

        public string GetInnerHtml(HtmlNode? node)
        {
            return node != null ? node.InnerHtml : string.Empty;
        }

        private HtmlNode GetNodeByXPath(string xPath)
        {
            return _htmlDoc.DocumentNode.SelectSingleNode(xPath);
        }

        private  HtmlNodeCollection GetNodesByXPath(string xPath)
        {
            return _htmlDoc.DocumentNode.SelectNodes(xPath);
        }

        private HtmlNode? GetChild(HtmlNode node, int position)
        {
            return node?.ChildNodes.Count() < position + 1 ? null : node?.ChildNodes.ElementAt(position);
        }

        private string GetAttributeValue(HtmlNode? node, string attributeName)
        {
            return node?.GetAttributeValue("class", string.Empty) ?? string.Empty;
        }
    }
}
