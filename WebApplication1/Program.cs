using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Đọc Connection String từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MyDBContext");

// Cấu hình SQL Server với Entity Framework Core
builder.Services.AddDbContext<MyDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
