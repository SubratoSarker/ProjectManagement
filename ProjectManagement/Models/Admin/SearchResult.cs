namespace ProjectManagement.Models.Admin
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string ResultType { get; set; }
        public int? TaskID { get; set; }
        public string Title { get; set; }
        public string? Snippet { get; set; }
        public string? ProjectName { get; set; }
        public string? ExtraInfo { get; set; }
        public DateTime SortDate { get; set; }
    }
}
