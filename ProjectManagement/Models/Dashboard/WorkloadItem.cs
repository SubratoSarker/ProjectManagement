using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models.Dashboard
{
    public class WorkloadItem
    {
        [Key]
        public int intUserID { get; set; }
        public string strUserName { get; set; }
        public int OpenTaskCount { get; set; }
    }
}
