using NSE.WebAPI.Core.Identidade;

namespace NSE.Identidade.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerConfiguration();
        }

        app.UseHttpsRedirection();

        app.UseAuthConfiguration();

        app.MapControllers();
        
        return app;
    }
}