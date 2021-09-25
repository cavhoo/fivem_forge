using System.ComponentModel.DataAnnotations;

namespace FiveMForge.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Key]
        public string Uuid { get; set; }
        public string Title { get; set; }
        public int Salary { get; set; }
    }
}