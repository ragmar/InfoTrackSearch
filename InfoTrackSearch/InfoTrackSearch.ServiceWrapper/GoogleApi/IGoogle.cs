namespace InfoTrack.ServiceWrapper.GoogleApi
{
    public interface IGoogleApi
    {
        Task<string> SearchPageWithElements(string searchTerm, int num);
    }
}
