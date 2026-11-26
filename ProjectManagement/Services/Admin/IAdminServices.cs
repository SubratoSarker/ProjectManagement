using ProjectManagement.Models.Admin;
using ProjectManagement.Models.Task;

namespace ProjectManagement.Services.Admin
{
    public interface IAdminServices
    {
        Task<List<AllUser>> GetAllUser(int UserID);
        Task<List<Team>> GetAllTeam();
        Task<string> UpdateUser(int User, string Name, string Phone, string Email, int Team, bool Active, bool isboss, int Enroll, bool Locked);
        Task<string> Create(string Name, int Enroll);
        Task<List<DateWiseReport>> GetTeamReport(int Enroll, DateTime From, DateTime To);
        Task<List<CompleatedReport>> CompleatedReport( DateTime From, DateTime To);
        Task<List<PendingTask>> GetPendingTasks(int Enroll);
        Task<List<Performance>> GetPerFormance(DateTime From, DateTime To, int User, int Project);

        // Offboarding / bulk task tooling
        Task<List<OffboardingUser>> GetAllUsersForOffboarding();
        Task<List<OpenTask>> GetOpenTasksByUser(int UserID);
        Task<BulkActionResult> BulkTaskAction(List<int> TaskIDs, int Mode, int TargetUser, int Enroll);

        // Activity log
        Task<List<ActivityLogEntry>> GetActivityLog(DateTime? From, DateTime? To);
    }

    public class BulkActionResult
    {
        public int SuccessCount { get; set; }
        public int Total { get; set; }
        public List<string> Failures { get; set; } = new();
    }
}
