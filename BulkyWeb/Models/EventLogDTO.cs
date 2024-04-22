namespace BulkyWeb.Models
{
    public class EventLogDTO
    {
        public Guid Id { get; set; }
        public string? TaskName { get; set; }
        public string? EventType { get; set; }
        public double? SecondsIntoTest { get; set; }
    }
}
