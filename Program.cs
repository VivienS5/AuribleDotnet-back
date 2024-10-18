using System.Text;
using Aurible.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur
builder.Services.AddControllers();

// Ajouter Swagger pour la documentation de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Aurible",
        Version = "V0.0.1",
        Description = "A simple Aurible app",
    });
});

// Configuration CORS - Autoriser toutes les origines
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
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IManageService, ManageService>();

// Configurer le DbContext pour utiliser PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Appliquer la politique CORS
app.UseCors("AllowAllOrigins");

app.UseAuthentication(); // Si vous utilisez une authentification
app.UseAuthorization();  // Nécessaire pour les contrôleurs

app.MapControllers(); // Mapper les contrôleurs

app.Run(); // Lancer l'application
