using FluentValidation;
using GoodHamburger.Application.Services;
using GoodHamburger.Application.Validators;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Data;
using GoodHamburger.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ── Database ─────────────────────────────────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GoodHamburgerDbContext>(options =>
    options.UseSqlite(connectionString));

// ── Controllers ─────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

// ── Swagger / OpenAPI ────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "Good Hamburger API",
        Version     = "v1",
        Description = "Sistema de pedidos da lanchonete Good Hamburger 🍔",
        Contact = new OpenApiContact
        {
            Name = "Good Hamburger Team",
            Email = "contact@goodhamburger.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// ── Dependency Injection ──────────────────────────────────────────────────────
// Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();

// Services
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Repositories
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// ── Database Migration ────────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var db = services.GetRequiredService<GoodHamburgerDbContext>();
        
        // Retry logic for database migration (useful when running in Docker)
        int retries = 5;
        while (retries > 0)
        {
            try
            {
                logger.LogInformation("Tentando aplicar migrações do banco de dados... (Tentativas restantes: {Retries})", retries);
                db.Database.Migrate();
                logger.LogInformation("Migrações aplicadas com sucesso.");
                break;
            }
            catch (Exception ex)
            {
                retries--;
                if (retries == 0) throw;
                logger.LogWarning("Falha ao conectar ao banco de dados. Tentando novamente em 5 segundos... Erro: {Message}", ex.Message);
                Thread.Sleep(5000);
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Um erro ocorreu ao aplicar as migrações do banco de dados.");
        if (!app.Environment.IsDevelopment()) throw;
    }
}

// ── Middleware ────────────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Good Hamburger API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

// ── Expose Program for integration tests ──────────────────────────────────────
public partial class Program { }