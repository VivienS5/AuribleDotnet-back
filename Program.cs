using Aurible.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AuribleDotnet_back.Interface;
using AuribleDotnet_back.Service.AuthServices;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur
builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Ajouter les services de l'application
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IChapterService, ChapterDbService>();
builder.Services.AddScoped<TTSService>();
builder.Services.AddScoped<IManageService, ManageService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configurer le DbContext pour utiliser PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Health Checks
builder.Services.AddHealthChecks();

builder.Services.AddScoped<IJwtTokenService, JWTService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AudioStreamingService>();
builder.Services.ConfigurationAuth(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Aurible",
        Version = "V0.0.0.0.0.0.1",
        Description = "A simple Aurible app",
    });
});
builder.Services.AddHttpClient();

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aurible V0.0.1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root dans l'environnement de développement
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aurible V0.0.1");
        c.RoutePrefix = "api-docs"; // Changer le chemin pour "/api-docs" en production
    });
}
app.UseHttpsRedirection();
// Activer CORS
app.UseCors("AllowAllOrigins");
// Optionnel : Activer l'authentification et l'autorisation JWT
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers(); // Mapper les contrôleurs

// Add Health Checks endpoint
app.MapHealthChecks("/health");

app.Run(); // Lancer l'application
