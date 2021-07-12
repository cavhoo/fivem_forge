namespace FiveMForge.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdminOnly { get; set; }
        public string Location { get; set; }
    }
}