var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PokeStream API",
        Version = "v1",
        Description = "API for PokéStream Collector - Interactive Pokémon overlay for streamers",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "PokeStream Team",
            Email = "support@pokestream.example.com"
        }
    });

    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokeStream API v1");
        c.RoutePrefix = "swagger"; // Available at /swagger
        c.DocumentTitle = "PokeStream API - Swagger UI";
        c.DefaultModelsExpandDepth(-1); // Hide models by default
    });

    // Swagger/OpenAPI via Swashbuckle (Microsoft.AspNetCore.OpenApi removed)
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();