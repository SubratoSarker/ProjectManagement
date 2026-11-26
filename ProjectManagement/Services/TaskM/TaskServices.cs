using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Forms;
using ProjectManagement.Models.Admin;
using ProjectManagement.Models.ProjectModel;
using ProjectManagement.Models.Task;
using ProjectManagement.Services.Session;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace ProjectManagement.Services.TaskM
{
    public class TaskServices : ITaskServices
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        public TaskServices(HttpClient httpClient, ISessionStorageService sessionStorageService)
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
        public async Task<string> CreateTask(string Name, string Description, string ReqFrom, int Priroty, DateOnly DeadLine, int Enroll, int Project)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/CreateTask?Name={Name}&Description={Description}&ReqFrom={ReqFrom}&Priroty={Priroty}&DeadLine={DeadLine:yyyy-MM-dd}&Enroll={Enroll}&Project={Project}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProjectPerUser>> GetProjectByUser(int Enroll, int Type)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<ProjectPerUser>>($"api/Task/GetProjectByUser?Enroll={Enroll}&Type={Type}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<Tasks>> GetTasks(int Enroll, int Status, int LabelID = 0)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Tasks>>($"api/Task/GetTasks?Enroll={Enroll}&Status={Status}&LabelID={LabelID}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<Tasks> GetCurrentTasks(int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<Tasks>($"api/Task/GetCurrentTasks?Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Steps>> GetStepsByTask(int Task)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Steps>>($"api/Task/GetStepsByTask?Task={Task}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<TransferUser>> TransferCheck(int Task, int User)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<TransferUser>>($"api/Task/TransferCheck?Task={Task}&User={User}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> StepManage(int Type, string Name, bool IsDone, int StepID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/StepManage?Type={Type}&Name={Name}&IsDone={IsDone}&StepID={StepID}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> TaskManage(int Type, int TaskID, int User, int Status, TimeSpan Working, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/TaskManage?Type={Type}&TaskID={TaskID}&User={User}&Status={Status}&Working={Working}&Enroll={Enroll}");
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
                var result = await _httpClient.GetFromJsonAsync<List<Performance>>($"api/Task/GetPerFormance?From={From}&To={To}&User={User}&Project={Project}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> Request(string TaskName, string Description, string RequestFrom, int USerID, int Project, DateTime Date, int Status, TimeSpan tm, string From, string To)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/Request?TaskName={TaskName}&Description={Description}&RequestFrom={RequestFrom}&USerID={USerID}&Project={Project}&Date={Date:yyyy-MM-ddTHH:mm:ss}&Status={Status}&tm={tm}&From={From}&To={To}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<Request>> GetRequest(int User, Boolean isSuperviser)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Request>>($"api/Task/GetRequestList?User={User}&isSuperviser={isSuperviser}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<Notify>> GetNotify(int User)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<Notify>>($"api/Task/GetNotify?Enroll={User}");
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
                var result = await _httpClient.GetFromJsonAsync<List<PendingTask>>($"api/Task/GetPendingTasks?Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<Tasks> GetTask(int TaskID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<Tasks>($"api/Task/GetTask?TaskID={TaskID}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> TaskUpdate(Tasks t, int USerID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/TaskUpdate?Task={t.intTaskID}&TaskName={t.strTaskName}&Description={t.strDescription}&RequestFrom={t.strRequestFrom}&Priroty={t.intPriroty}&DeadLine={t.dteDeadLine:yyyy-MM-dd}&USerID={USerID}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<CompleatedReport>> CompleatedReport(DateTime From, DateTime To, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<CompleatedReport>>($"api/Task/GetCompleatedReport?From={From}&To={To}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        // Implementation
        public async Task<string> RequestTimeAdjustment(
            int taskId, int userId, DateTime date, int status,
            TimeSpan duration, TimeOnly fromTime, TimeOnly toTime, string reason)
        {
            try
            {
                var url = $"api/Task/RequestForTask" +
                          $"?TaskID={taskId}" +
                          $"&UserID={userId}" +
                          $"&Date={date:yyyy-MM-ddTHH:mm:ss}" +
                          $"&Status={status}" +
                          $"&tm={duration:hh\\:mm\\:ss}" +
                          $"&From={Uri.EscapeDataString(fromTime.ToString("HH:mm"))}" +
                          $"&To={Uri.EscapeDataString(toTime.ToString("HH:mm"))}" +
                          $"&Reason={Uri.EscapeDataString(reason)}";

                var response = await _httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ---- Comments ----
        public async Task<string> AddComment(int TaskID, int Enroll, string Comment)
        {
            try
            {
                await SetAuthorizationHeader();
                var url = $"api/Task/AddComment?TaskID={TaskID}&Enroll={Enroll}&Comment={Uri.EscapeDataString(Comment)}";
                var result = await _httpClient.GetFromJsonAsync<string>(url);
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<TaskComment>> GetComments(int TaskID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<TaskComment>>($"api/Task/GetComments?TaskID={TaskID}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // ---- Attachments ----
        public async Task<string> UploadAttachment(int TaskID, int Enroll, IBrowserFile File)
        {
            try
            {
                await SetAuthorizationHeader();
                using var content = new MultipartFormDataContent();
                using var stream = File.OpenReadStream(maxAllowedSize: 25_000_000);
                using var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrEmpty(File.ContentType) ? "application/octet-stream" : File.ContentType);
                content.Add(fileContent, "File", File.Name);

                var response = await _httpClient.PostAsync($"api/Task/UploadAttachment?TaskID={TaskID}&Enroll={Enroll}", content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<TaskAttachment>> GetAttachments(int TaskID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<TaskAttachment>>($"api/Task/GetAttachments?TaskID={TaskID}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> DeleteAttachment(int AttachmentID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/DeleteAttachment?AttachmentID={AttachmentID}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<byte[]> DownloadAttachment(int TaskID, string FileName)
        {
            try
            {
                await SetAuthorizationHeader();
                var response = await _httpClient.GetAsync($"api/Task/DownloadAttachment?TaskID={TaskID}&FileName={Uri.EscapeDataString(FileName)}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // ---- Subtasks ----
        public async Task<string> CreateSubtask(string Name, string Description, int ParentTaskID, int Priroty, DateOnly DeadLine, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var url = $"api/Task/CreateSubtask?Name={Uri.EscapeDataString(Name)}&Description={Uri.EscapeDataString(Description)}&ParentTaskID={ParentTaskID}&Priroty={Priroty}&DeadLine={DeadLine:yyyy-MM-dd}&Enroll={Enroll}";
                var result = await _httpClient.GetFromJsonAsync<string>(url);
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<SubtaskItem>> GetSubtasks(int ParentTaskID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<SubtaskItem>>($"api/Task/GetSubtasks?ParentTaskID={ParentTaskID}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // ---- Dependencies ----
        public async Task<List<SubtaskItem>> GetTasksForDependencyPicker(int TaskID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<SubtaskItem>>($"api/Task/GetTasksForDependencyPicker?TaskID={TaskID}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> AddDependency(int TaskID, int DependsOnTaskID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/AddDependency?TaskID={TaskID}&DependsOnTaskID={DependsOnTaskID}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> RemoveDependency(int DependencyID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<string>($"api/Task/RemoveDependency?DependencyID={DependencyID}&Enroll={Enroll}");
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<TaskDependency>> GetDependencies(int TaskID)
        {
            try
            {
                await SetAuthorizationHeader();
                var result = await _httpClient.GetFromJsonAsync<List<TaskDependency>>($"api/Task/GetDependencies?TaskID={TaskID}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // ---- Teammate task visibility ----
        public async Task<List<OpenTask>> GetOpenTasksForUser(int UserID)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<OpenTask>>($"api/Task/GetOpenTasksForUser?UserID={UserID}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // ---- Labels ----
        public async Task<string> CreateLabel(string Name, string Color, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                var url = $"api/Task/CreateLabel?Name={Uri.EscapeDataString(Name)}&Color={Uri.EscapeDataString(Color)}&Enroll={Enroll}";
                return await _httpClient.GetFromJsonAsync<string>(url);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<TaskLabel>> GetLabels()
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<TaskLabel>>("api/Task/GetLabels");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<TaskLabel>> GetLabelsByTask(int TaskID)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<TaskLabel>>($"api/Task/GetLabelsByTask?TaskID={TaskID}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> AssignLabel(int TaskID, int LabelID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<string>($"api/Task/AssignLabel?TaskID={TaskID}&LabelID={LabelID}&Enroll={Enroll}");
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> RemoveLabel(int TaskID, int LabelID, int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<string>($"api/Task/RemoveLabel?TaskID={TaskID}&LabelID={LabelID}&Enroll={Enroll}");
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<Teammate>> GetTeammates(int Enroll)
        {
            try
            {
                await SetAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<Teammate>>($"api/Task/GetTeammates?Enroll={Enroll}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
