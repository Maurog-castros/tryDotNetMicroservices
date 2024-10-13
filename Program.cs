var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar soporte para controladores tradicionales
builder.Services.AddControllers();

var app = builder.Build();

// Configurar el pipeline HTTP
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configurar autorización (si fuera necesario)
app.UseAuthorization();

// Mapear los controladores tradicionales
app.MapControllers();

// Endpoint minimalista "weatherforecast"
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Ejecutar la aplicación
app.Run();

// Definición del tipo de registro WeatherForecast
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
