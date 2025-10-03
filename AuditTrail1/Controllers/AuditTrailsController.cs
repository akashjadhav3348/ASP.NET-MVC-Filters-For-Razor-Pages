using AuditTrail1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuditTrail1.Controllers
{
    public class AuditTrailsController : Controller
    {
        private readonly string _jsonFilePath = "auditlogs.json";

        public IActionResult Index(string userName, string module, DateTime? startDate, DateTime? endDate)
        {
            List<AuditTrail> logs = new();

            // Read data from JSON file
            if (System.IO.File.Exists(_jsonFilePath))
            {
                var json = System.IO.File.ReadAllText(_jsonFilePath);
                logs = JsonSerializer.Deserialize<List<AuditTrail>>(json) ?? new List<AuditTrail>();
            }

            var filteredLogs = logs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userName))
                filteredLogs = filteredLogs.Where(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(module))
                filteredLogs = filteredLogs.Where(x => x.Module.Equals(module, StringComparison.OrdinalIgnoreCase));

            if (startDate.HasValue)
                filteredLogs = filteredLogs.Where(x => x.Timestamp >= startDate.Value);

            if (endDate.HasValue)
                filteredLogs = filteredLogs.Where(x => x.Timestamp <= endDate.Value);

            var result = filteredLogs.OrderByDescending(x => x.Timestamp).ToList();

            // Distinct dropdown data
            ViewBag.Users = logs.Select(x => x.UserName).Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();
            ViewBag.Modules = logs.Select(x => x.Module).Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToList();

            // Keep filter state
            ViewBag.UserName = userName;
            ViewBag.Module = module;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(result);
        }
    }
}
