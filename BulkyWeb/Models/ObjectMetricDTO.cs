namespace BulkyWeb.Models
{
    public class ObjectMetricDTO
    {
        public string? objectName {  get; set; }
        public string? description { get; set; }
        public List<int>? associatedTaskIndexes { get; set; }
        public string? activeTaskWhileObjectGrabbed { get; set; }
        public string? activeTaskDescription { get; set; }
    }
}
