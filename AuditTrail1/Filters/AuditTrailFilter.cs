using AuditTrail1.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

public class AuditTrailFilter : IActionFilter
{
    private readonly string _jsonFilePath = "auditlogs.json";
    private readonly string? _module;

    public AuditTrailFilter(string? module = null)
    {
        _module = module;
    }

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var userName = context.HttpContext?.User?.Identity?.Name ?? "Anonymous";
        var controllerName = context.ActionDescriptor.RouteValues["controller"] ?? "UnknownController";
        var actionName = context.ActionDescriptor.RouteValues["action"] ?? "UnknownAction";
        var httpMethod = context.HttpContext?.Request.Method ?? "GET";

        if (httpMethod == "GET" && actionName.ToLower() == "index") return;

        var moduleName = _module ?? controllerName;

        var log = new AuditTrail
        {
            Id = 0, // optional, auto-increment later
            UserName = userName,
            Module = moduleName,
            Action = $"{actionName} ({controllerName})",
            Timestamp = DateTime.Now
        };

        List<AuditTrail> logs = new();
        if (File.Exists(_jsonFilePath))
        {
            var json = File.ReadAllText(_jsonFilePath);
            logs = JsonSerializer.Deserialize<List<AuditTrail>>(json) ?? new List<AuditTrail>();
        }

        log.Id = logs.Any() ? logs.Max(x => x.Id) + 1 : 1;
        logs.Add(log);

        File.WriteAllText(_jsonFilePath, JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true }));
    }
}
