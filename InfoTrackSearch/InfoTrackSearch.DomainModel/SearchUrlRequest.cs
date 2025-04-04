using System.ComponentModel;

namespace DomainModel
{
    public class SearchUrlRequest
    {
        [DefaultValue("land registry search")]
        public string SearchTerm { get; set; }

        [DefaultValue("www.infotrack.co.uk")]
        public string UrlToSearch { get; set; }
    }
}
