using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BulkyWeb.Models
{
    public class EventLog
    {
        [Key]
        public Guid Id { get; set; }
        public string? TaskName { get; set; }
        public string? EventType { get; set; }
        public double? SecondsIntoTest { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public Student? Student { get; set; } 
    }
}
