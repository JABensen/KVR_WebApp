using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BulkyWeb.Models
{
    public class AssociatedTaskIndex
    {
        [Key]
        public Guid Id { get; set; } //Primary Key
        public int Index { get; set; }

        [ForeignKey("ObjectMetric")]
        public Guid MetricId { get; set; } //Foreign Key
        public ObjectMetric? ObjectMetric { get; set; } //Navigation Property
    }
}
