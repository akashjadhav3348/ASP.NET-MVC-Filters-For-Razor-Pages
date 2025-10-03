using AuditTrail1.Models;
using System.Text.Json;

namespace AuditTrail1.Data
{
    public class JsonAuditTrailRepository
    {
        private readonly string _filePath;

        public JsonAuditTrailRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "auditlogs.json");

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<AuditTrail> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<AuditTrail>>(json) ?? new List<AuditTrail>();
        }

        public void Add(AuditTrail log)
        {
            var logs = GetAll();
            log.Id = logs.Any() ? logs.Max(x => x.Id) + 1 : 1;
            logs.Add(log);
            var json = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
