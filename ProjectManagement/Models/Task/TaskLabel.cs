using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models.Task
{
    public class TaskLabel
    {
        [Key]
        public int intLabelID { get; set; }
        public string strName { get; set; }
        public string strColor { get; set; }
    }
}
