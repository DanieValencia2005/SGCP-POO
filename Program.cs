using Microsoft.EntityFrameworkCore;
using SGCP_POO.Models;

var builder = WebApplication.CreateBuilder(args);

// Obtener la URL de PostgreSQL desde Render
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

// Convertirla a formato que Npgsql entiende
string connectionString;
if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};Pooling=true;Trust Server Certificate=true";
}
else
{
    // Si no existe la variable, usa tu conexión local
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Registrar DbContext
builder.Services.AddDbContext<SGCPContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Escuchar en el puerto de Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
