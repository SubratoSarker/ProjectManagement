using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models.Dashboard
{
    public class DashboardSummary
    {
        [Key]
        public int intStatusID { get; set; }
        public string strStatus { get; set; }
        public int TaskCount { get; set; }
    }
}
