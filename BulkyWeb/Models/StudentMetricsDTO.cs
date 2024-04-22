namespace BulkyWeb.Models
{
    public class StudentMetricsDTO
    {
        public string? StudentName { get; set; }
        public List<EventLogDTO>? EventLogs {  get; set; }
    }
}
