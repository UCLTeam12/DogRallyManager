using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class CorsOptionsMiddleware
{
    private readonly RequestDelegate _next;

    public CorsOptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == "OPTIONS")
        {
            // Return a 200 OK response with appropriate CORS headers
            context.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:7142");
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");

            // End the request pipeline
            context.Response.StatusCode = 200;
            await context.Response.CompleteAsync();
        }
        else
        {
            // Pass the request to the next middleware in the pipeline
            await _next(context);
        }
    }
}