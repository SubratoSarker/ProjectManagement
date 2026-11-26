using ProjectManagement.Models.Dashboard;
using ProjectManagement.Models.ProjectModel;

namespace ProjectManagement.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<List<DashboardSummary>> GetSummary(int Enroll, int ProjectID = 0);
        Task<List<WorkloadItem>> GetWorkload(int Enroll, int ProjectID = 0);
        Task<List<OverdueTask>> GetOverdue(int Enroll, int ProjectID = 0);
        Task<List<ProjectProgress>> GetProjectProgress(int Enroll, int ProjectID = 0);
        Task<List<UpcomingMilestone>> GetUpcomingMilestones(int Enroll);
        Task<List<TeamActivityMember>> GetTeamActivity(int Enroll);
        Task<List<CalendarTask>> GetCalendarTasks(int Enroll, DateOnly From, DateOnly To);
    }
}
