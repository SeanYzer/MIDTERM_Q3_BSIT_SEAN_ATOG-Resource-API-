using Auth_Level2_Cookie.Models;
using Auth_Level2_Cookie.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;

namespace Auth_Level2_Cookie.Middleware;


public class SomeRandomMiddleware
{
    private readonly RequestDelegate _next;

    public SomeRandomMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
       
        var temp = context.Request.Headers;
        Console.WriteLine("Hello from SomeRandomMiddleware");
    }
}


public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, AppDbContext db) // Idagdag ang AppDbContext dito
    {
        // 1. Handle missing, null, or empty Authorization header
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing Authorization Header");
            return;
        }

        try
        {
            // 2. Parse the Authorization header (Expected: "Basic [Base64String]")
            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);

            // 3. Decode from Base64
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            // 4. Search 'User' table in SQLite using decoded values
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Username or Password");
                return;
            }

            // Success! Attach user to context items
            context.Items["User"] = user;
        }
        catch
        {
            // Handle malformed Base64 or header
            context.Response.StatusCode = 401;
            return;
        }

        await _next(context);
    }
}