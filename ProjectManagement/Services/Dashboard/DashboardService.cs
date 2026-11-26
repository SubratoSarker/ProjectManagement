using Blazored.SessionStorage;
using ProjectManagement.Models.Dashboard;
using ProjectManagement.Models.ProjectModel;
using ProjectManagement.Services.Session;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectManagement.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        // The API's [ApiController] JSON serializer camelCases property names (e.g. TaskCount -> taskCount).
        // Most models elsewhere in this app are unaffected because their properties already start lowercase
        // (intTaskID, strTaskName...), but these aggregate fields (TaskCount, OpenTaskCount, TotalTasks,
        // DoneTasks) start uppercase, and GetFromJsonAsync's default options are case-sensitive -- without
        // this, every one of those fields silently deserializes to 0.
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        public DashboardService(HttpClient httpClient, ISessionStorageService sessionStorageService)
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

        public async Task<List<DashboardSummary>> GetSummary(int Enroll, int ProjectID = 0)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<DashboardSummary>>($"api/Dashboard/GetSummary?Enroll={Enroll}&ProjectID={ProjectID}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<WorkloadItem>> GetWorkload(int Enroll, int ProjectID = 0)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<WorkloadItem>>($"api/Dashboard/GetWorkload?Enroll={Enroll}&ProjectID={ProjectID}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<OverdueTask>> GetOverdue(int Enroll, int ProjectID = 0)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<OverdueTask>>($"api/Dashboard/GetOverdue?Enroll={Enroll}&ProjectID={ProjectID}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProjectProgress>> GetProjectProgress(int Enroll, int ProjectID = 0)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<ProjectProgress>>($"api/Dashboard/GetProjectProgress?Enroll={Enroll}&ProjectID={ProjectID}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UpcomingMilestone>> GetUpcomingMilestones(int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<UpcomingMilestone>>($"api/Dashboard/GetUpcomingMilestones?Enroll={Enroll}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<TeamActivityMember>> GetTeamActivity(int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<TeamActivityMember>>($"api/Dashboard/GetTeamActivity?Enroll={Enroll}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<CalendarTask>> GetCalendarTasks(int Enroll, DateOnly From, DateOnly To)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<CalendarTask>>(
                    $"api/Dashboard/GetCalendarTasks?Enroll={Enroll}&From={From:yyyy-MM-dd}&To={To:yyyy-MM-dd}", _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
