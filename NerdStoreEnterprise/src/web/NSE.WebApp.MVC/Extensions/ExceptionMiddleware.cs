using System.Net;
using Refit;

namespace NSE.WebApp.MVC.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpRequestException ex)
        {
            HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (ValidationApiException ex)
        {
            HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (ApiException ex)
        {
            HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (Polly.CircuitBreaker.BrokenCircuitException)
        {
            HandleCircuitBreakerExceptionAsync(httpContext);
        }
    }

    private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return;
        }

        context.Response.StatusCode = (int)statusCode;
    }
    
    private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
    {
        context.Response.Redirect("/sistema-indisponivel");
    }
}