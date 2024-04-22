using Microsoft.EntityFrameworkCore;
using BulkyWeb.Models;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Professor> Professors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<ObjectMetric> ObjectMetrics { get; set; }
        public DbSet<AssociatedTaskIndex> AssociatedTasks { get; set; }
    }
}
