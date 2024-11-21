using Microsoft.EntityFrameworkCore;
using RinconGuatemaltecoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Registrar el contexto de base de datos
builder.Services.AddDbContext<RinconGuatemaltecoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Configurar sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar middleware
app.UseStaticFiles();
app.UseRouting();
app.UseSession();



// Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"); // Configura el inicio de sesión como página inicial




app.Run();
