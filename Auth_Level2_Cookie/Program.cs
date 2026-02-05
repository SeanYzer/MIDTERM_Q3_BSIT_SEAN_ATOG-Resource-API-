using Auth_Level2_Cookie.Data; // Siguraduhing tama ang namespace na ito
using Auth_Level2_Cookie.Middleware;
using Auth_Level2_Cookie.Data;
using Auth_Level2_Cookie.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. SERVICES CONFIGURATION
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// SWAGGER CONFIG: Para lumabas ang "Authorize" button sa Swagger UI
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header gamit ang Base64 (Username:Password)."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" }
            },
            new string[] {}
        }
    });
});

// DATABASE CONFIG: SQLite Requirement
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=auth.db"));

var app = builder.Build();

// 2. MIDDLEWARE PIPELINE
app.UseSwagger();
app.UseSwaggerUI();

// SELF-INSTANTIATION: Automatic na paggawa ng database table
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

// CUSTOM MIDDLEWARE: Ito ang mag-decode ng Base64 at mag-check sa SQLite
app.UseMiddleware<BasicAuthMiddleware>();

app.MapControllers();

app.Run();