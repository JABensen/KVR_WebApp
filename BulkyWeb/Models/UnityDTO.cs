using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class UnityDTO
    {
        public string? playerName {  get; set; }
        public string? playerKey { get; set; }
        public List<ObjectMetricDTO>? incorrectlyGrabbedObjects { get; set; }
        public List<ActivityEventDTO>? eventLogs { get; set; }
    }
}
