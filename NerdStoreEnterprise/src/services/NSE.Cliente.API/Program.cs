using NSE.AspireApp.ServiceDefaults;
using NSE.Cliente.API.Configuration;
using NSE.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.RegisterServices();

var app = builder.Build();
app.UseSwaggerConfiguration();
app.UseApiConfiguration();
app.MapDefaultEndpoints();
app.Run();