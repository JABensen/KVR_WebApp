using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class ProfessorLoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
