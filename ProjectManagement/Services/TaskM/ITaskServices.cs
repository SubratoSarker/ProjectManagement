using Microsoft.AspNetCore.Components.Forms;
using ProjectManagement.Models.Admin;
using ProjectManagement.Models.Task;

namespace ProjectManagement.Services.TaskM
{
    public interface ITaskServices
    {
        Task<List<ProjectPerUser>> GetProjectByUser(int Enroll, int Type);
        Task<string> CreateTask(string Name, string Description, string ReqFrom, int Priroty, DateOnly DeadLine, int Enroll, int Project);
        Task<List<Tasks>> GetTasks(int Enroll, int Status, int LabelID = 0);
        Task<Tasks> GetCurrentTasks(int Enroll);
        Task<List<Steps>> GetStepsByTask(int Task);
        Task<List<TransferUser>> TransferCheck(int Task, int User);
        Task<string> StepManage(int Type, string Name, bool IsDone, int StepID, int Enroll);
        Task<string> TaskManage(int Type, int TaskID, int User, int Status, TimeSpan Working, int Enroll);
        Task<List<Performance>> GetPerFormance(DateTime From, DateTime To, int User, int Project);
        Task<string> Request(string TaskName, string Description, string RequestFrom, int USerID, int Project, DateTime Date, int Status, TimeSpan tm, string From, string To);
        Task<List<Request>> GetRequest(int User, Boolean isSuperviser);
        Task<List<Notify>> GetNotify(int User);
        Task<List<PendingTask>> GetPendingTasks(int Enroll);
        Task<Tasks> GetTask(int TaskID);
        Task<string> TaskUpdate(Tasks task, int USerID);
        Task<List<CompleatedReport>> CompleatedReport(DateTime From, DateTime To, int Enroll);
        Task<string> RequestTimeAdjustment( int taskId, int userId, DateTime date, int status, TimeSpan duration, TimeOnly fromTime, TimeOnly toTime, string reason);

        // Comments
        Task<string> AddComment(int TaskID, int Enroll, string Comment);
        Task<List<TaskComment>> GetComments(int TaskID);

        // Attachments
        Task<string> UploadAttachment(int TaskID, int Enroll, IBrowserFile File);
        Task<List<TaskAttachment>> GetAttachments(int TaskID);
        Task<string> DeleteAttachment(int AttachmentID, int Enroll);
        Task<byte[]> DownloadAttachment(int TaskID, string FileName);

        // Subtasks
        Task<string> CreateSubtask(string Name, string Description, int ParentTaskID, int Priroty, DateOnly DeadLine, int Enroll);
        Task<List<SubtaskItem>> GetSubtasks(int ParentTaskID);

        // Dependencies
        Task<List<SubtaskItem>> GetTasksForDependencyPicker(int TaskID);
        Task<string> AddDependency(int TaskID, int DependsOnTaskID, int Enroll);
        Task<string> RemoveDependency(int DependencyID, int Enroll);
        Task<List<TaskDependency>> GetDependencies(int TaskID);

        // Teammate task visibility
        Task<List<OpenTask>> GetOpenTasksForUser(int UserID);

        // Labels
        Task<string> CreateLabel(string Name, string Color, int Enroll);
        Task<List<TaskLabel>> GetLabels();
        Task<List<TaskLabel>> GetLabelsByTask(int TaskID);
        Task<string> AssignLabel(int TaskID, int LabelID, int Enroll);
        Task<string> RemoveLabel(int TaskID, int LabelID, int Enroll);

        // Teammates (for tagging "@Name" mentions in comments)
        Task<List<Teammate>> GetTeammates(int Enroll);
    }
}
