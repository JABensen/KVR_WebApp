using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BulkyWeb.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        public string? StudentName { get; set;}

        [ForeignKey("ClassKey")]
        public string? ClassKey { get; set; } //Foreign key linking Student to Professor
        public Professor? Professor { get; set; } //Navigation property
        public ICollection<EventLog>? EventLogs { get; set; }
        public ICollection<ObjectMetric>? ObjectMetrics { get; set; }
    }
}
