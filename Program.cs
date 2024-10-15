var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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

// Optionnel : Ajouter authentification JWT si nécessaire
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ton_issuer",
            ValidAudience = "ton_audience",
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes("ta_clé_secrète_super_sécurisée"))
        };
    });

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aurible V0.0.0.0.0.0.1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root.
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aurible V0.0.0.0.0.0.1");
        c.RoutePrefix = "api-docs"; // Change the route to "/api-docs" in production for better security.
    });
}

app.UseHttpsRedirection();

// Activer CORS
app.UseCors("AllowAllOrigins");

// Optionnel : Activer l'authentification et l'autorisation JWT
app.UseAuthentication(); // JWT, si nécessaire
app.UseAuthorization();

app.MapControllers();

app.Run();
