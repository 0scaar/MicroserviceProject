using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Data;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Cliente.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ClientesContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
    }
    
    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");
        
        app.UseAuthConfiguration();

        app.MapControllers();
        
        return app;
    }
}