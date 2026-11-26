using Blazored.SessionStorage;
using ProjectManagement.Models.Admin;
using ProjectManagement.Services.Session;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectManagement.Services.Search
{
    public class SearchService : ISearchService
    {
        // Same reason as DashboardService -- the API's [ApiController] JSON serializer camelCases
        // property names (ResultType -> resultType), and GetFromJsonAsync defaults to case-sensitive.
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        public SearchService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }

        private async Task SetAuthorizationHeader()
        {
            var session = await _sessionStorageService.GetItemAsync<SessionServices>("session");
            if (session != null && !string.IsNullOrEmpty(session.Key))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Key);
            }
        }

        public async Task<List<SearchResult>> Global(int Enroll, string Keyword)
        {
            try
            {
                await SetAuthorizationHeader();
                var encoded = Uri.EscapeDataString(Keyword);
                return await _httpClient.GetFromJsonAsync<List<SearchResult>>($"api/Search/Global?Enroll={Enroll}&Keyword={encoded}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
