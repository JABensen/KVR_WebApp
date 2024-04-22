using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BulkyWeb.Models
{
    public class ObjectMetric
    {
        [Key]
        public Guid Id { get; set; }
        public string? ObjectName { get; set; }
        public string? Description { get; set;}
        public string? ActiveTask { get; set; }
        public string? ActiveTaskDescription { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public Student? Student { get; set; } //Navigation property
        public ICollection<AssociatedTaskIndex>? associatedTaskIndexes { get; set; }
    }
}
