using HtmlAgilityPack;

namespace InfoTrack.ServiceWrapper.HtmlHelper
{
    public interface IHtmlHelper
    {
        HtmlNodeCollection GenerateHtmlNodes(string html);
        string GetInnerHtml(HtmlNode? node);
    }
}
