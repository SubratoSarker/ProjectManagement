using ProjectManagement.Models.ProjectModel;
using ProjectManagement.Services.Admin;

namespace ProjectManagement.Services.Project
{
    public interface IProjectService
    {
        Task<string> Create(string Name, DateOnly DeadLIne, int Enroll);
        Task<List<Projects>> GetProjectByTeam(int TeamID, int Enroll);
        Task<string> UdateStatus(int Type, int Project, int Enroll);
        Task<List<UserPerProject>> GetUserPerProject(int TeamID, int ProjectID);
        Task<string> AssigneUser(int User, int Project, bool Active, bool KeyPerson, int Enroll);
        Task<string> RequestAction(int RequestID, int Enroll, int Type);
        Task<BulkActionResult> BulkRequestAction(List<int> RequestIDs, int Enroll, int Type);

        // Milestones
        Task<string> CreateMilestone(int ProjectID, string Name, DateOnly Date, int Enroll);
        Task<List<Milestone>> GetMilestones(int ProjectID);
        Task<string> ToggleMilestone(int MilestoneID, int Enroll);
        Task<string> DeleteMilestone(int MilestoneID, int Enroll);

        // Timeline
        Task<List<TimelineTask>> GetTimeline(int ProjectID);
    }
}
