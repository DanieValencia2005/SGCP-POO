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
    // Usar el puerto de la URL o 5432 por defecto
    int dbPort = uri.Port != -1 ? uri.Port : 5432;
    connectionString = $"Host={uri.Host};Port={dbPort};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};Pooling=true;Trust Server Certificate=true";
}
else
{
    // Si no existe la variable, usar conexión local
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Registrar DbContext
builder.Services.AddDbContext<SGCPContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllersWithViews();

// Configuración de sesiones
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
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Aplicar migraciones automáticas
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SGCPContext>();
    db.Database.Migrate();
}

// Configurar puerto para Render (o fallback 5000)
var portEnv = Environment.GetEnvironmentVariable("PORT");
var portToUse = !string.IsNullOrEmpty(portEnv) ? portEnv : "5000";
app.Urls.Add($"http://0.0.0.0:{portToUse}");

app.Run();
