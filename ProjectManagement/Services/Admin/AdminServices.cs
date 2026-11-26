using Blazored.SessionStorage;
using ProjectManagement.Models.Admin;
using ProjectManagement.Models.Task;
using ProjectManagement.Services.Session;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectManagement.Services.Admin
{
    public class AdminServices : IAdminServices
    {
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;

        public AdminServices(HttpClient httpClient, ISessionStorageService sessionStorageService)
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
        public async Task<List<AllUser>> GetAllUser(int UserID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<AllUser>>($"api/Admin/GetAllUser?UserID={UserID}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<Team>> GetAllTeam()
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Team>>($"api/Admin/GetAllTeam");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> UpdateUser(int User, string Name, string Phone, string Email, int Team, bool Active, bool isboss, int Enroll, bool Locked)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Admin/UpdateUser?User={User}&Name={Name}&Phone={Phone}&Email={Email}&Team={Team}&Active={Active}&isboss={isboss}&Enroll={Enroll}&Locked={Locked}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Create(string Name, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Admin/Create?Name={Name}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<DateWiseReport>> GetTeamReport(int Enroll, DateTime From, DateTime To)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<DateWiseReport>>($"api/Admin/GetTeamReport?Enroll={Enroll}&From={From}&To={To}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<CompleatedReport>> CompleatedReport(DateTime From, DateTime To)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<CompleatedReport>>($"api/Admin/GetCompleatedReport?From={From}&To={To}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<PendingTask>> GetPendingTasks(int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<PendingTask>>($"api/Admin/GetPendingTasks?Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<Performance>> GetPerFormance(DateTime From, DateTime To, int User, int Project)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Performance>>($"api/Admin/GetPerFormance?From={From}&To={To}&User={User}&Project={Project}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // ---- Offboarding / bulk task tooling ----
        public async Task<List<OffboardingUser>> GetAllUsersForOffboarding()
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<OffboardingUser>>("api/Admin/GetAllUsersForOffboarding");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<OpenTask>> GetOpenTasksByUser(int UserID)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<OpenTask>>($"api/Admin/GetOpenTasksByUser?UserID={UserID}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<BulkActionResult> BulkTaskAction(List<int> TaskIDs, int Mode, int TargetUser, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var query = string.Join("&", TaskIDs.Select(id => $"TaskIDs={id}"));
                var url = $"api/Admin/BulkTaskAction?{query}&Mode={Mode}&TargetUser={TargetUser}&Enroll={Enroll}";
                return await _httpClient.GetFromJsonAsync<BulkActionResult>(url, _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new BulkActionResult { Failures = { ex.Message } };
            }
        }

        // ---- Activity log ----
        public async Task<List<ActivityLogEntry>> GetActivityLog(DateTime? From, DateTime? To)
        {
            try
            {
                await SetAuthorizationHeader();
                var url = "api/Admin/GetActivityLog";
                var query = new List<string>();
                if (From.HasValue) { query.Add($"From={From:yyyy-MM-ddTHH:mm:ss}"); }
                if (To.HasValue) { query.Add($"To={To:yyyy-MM-ddTHH:mm:ss}"); }
                if (query.Any()) { url += "?" + string.Join("&", query); }
                return await _httpClient.GetFromJsonAsync<List<ActivityLogEntry>>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
