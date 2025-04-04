using DomainModel;

namespace Services.UrlPosition
{
    public interface IUrlPosition
    {
        Task<string> CalculatePositionUrl(SearchUrlRequest request);
    }
}
