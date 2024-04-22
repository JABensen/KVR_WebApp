using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Professor
    {
        [Key]
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? HashedPassword { get; set; }
        public byte[]? Salt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? ClassKey { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
