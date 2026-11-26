using ProjectManagement.Models.Admin;

namespace ProjectManagement.Services.Search
{
    public interface ISearchService
    {
        Task<List<SearchResult>> Global(int Enroll, string Keyword);
    }
}
