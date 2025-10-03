using AuditTrail1.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext
//builder.Services.AddDbContext<AuditTrailContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Use JSON Repository for demo
builder.Services.AddSingleton<JsonAuditTrailRepository>();

// Register Action Filter
builder.Services.AddScoped<AuditTrailFilter>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AuditTrails}/{action=Index}/{id?}");

app.Run();
