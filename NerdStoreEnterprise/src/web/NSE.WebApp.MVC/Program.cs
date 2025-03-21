using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSE.AspireApp.ServiceDefaults;
using NSE.WebApp.MVC.Configuration;
using NSE.WebApp.MVC.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Agregar configuración para diferentes ambientes
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Cargar secretos de usuario en desarrollo
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentityConfiguration();
builder.Services.AddMvcConfiguration(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseMvcConfiguration();

app.MapDefaultEndpoints();

app.Run();