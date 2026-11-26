using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Blazored.SessionStorage;
using ProjectManagement.Models.ProjectModel;
using ProjectManagement.Models.User;
using ProjectManagement.Services.Admin;
using ProjectManagement.Services.Session;

namespace ProjectManagement.Services.Project
{
    public class ProjectService : IProjectService
    {
        // The API's [ApiController] JSON serializer camelCases property names (SuccessCount ->
        // successCount), and GetFromJsonAsync defaults to case-sensitive -- needed for BulkActionResult.
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        public ProjectService(HttpClient httpClient, ISessionStorageService sessionStorageService)
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
        public async Task<string> AssigneUser(int User, int Project, bool Active, bool KeyPerson, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Project/ProjectAssign?User={User}&Project={Project}&Active={Active}&KeyPerson={KeyPerson}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> Create(string Name, DateOnly DeadLine, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Project/Create?Name={Name}&DeadLine={DeadLine:yyyy-MM-dd}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<Projects>> GetProjectByTeam(int TeamID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Projects>>($"api/Project/GetProjectByTeam?TeamID={TeamID}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UserPerProject>> GetUserPerProject(int TeamID, int ProjectID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<UserPerProject>>($"api/Project/UserPerProject?TeamID={TeamID}&ProjectID={ProjectID}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> UdateStatus(int Type, int Project, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Project/StausUpdate?Type={Type}&Project={Project}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> RequestAction(int RequestID, int Enroll, int Type)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Project/RequestAction?RequestID={RequestID}&Enroll={Enroll}&Type={Type}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<BulkActionResult> BulkRequestAction(List<int> RequestIDs, int Enroll, int Type)
        {
            try
            {
                await SetAuthorizationHeader();
                var query = string.Join("&", RequestIDs.Select(id => $"RequestIDs={id}"));
                var url = $"api/Project/BulkRequestAction?{query}&Enroll={Enroll}&Type={Type}";
                return await _httpClient.GetFromJsonAsync<BulkActionResult>(url, _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new BulkActionResult { Failures = { ex.Message } };
            }
        }

        // ---- Milestones ----
        public async Task<string> CreateMilestone(int ProjectID, string Name, DateOnly Date, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var url = $"api/Project/CreateMilestone?ProjectID={ProjectID}&Name={Uri.EscapeDataString(Name)}&Date={Date:yyyy-MM-dd}&Enroll={Enroll}";
                var result = await _httpClient.GetFromJsonAsync<string>(url);
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<Milestone>> GetMilestones(int ProjectID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Milestone>>($"api/Project/GetMilestones?ProjectID={ProjectID}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> ToggleMilestone(int MilestoneID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Project/ToggleMilestone?MilestoneID={MilestoneID}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> DeleteMilestone(int MilestoneID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Project/DeleteMilestone?MilestoneID={MilestoneID}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ---- Timeline ----
        public async Task<List<TimelineTask>> GetTimeline(int ProjectID)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<TimelineTask>>($"api/Project/GetTimeline?ProjectID={ProjectID}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
