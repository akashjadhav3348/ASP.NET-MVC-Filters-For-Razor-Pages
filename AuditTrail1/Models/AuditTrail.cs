namespace AuditTrail1.Models
{
    public class AuditTrail
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
