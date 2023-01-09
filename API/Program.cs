var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var app = builder.Build();

//Request Pipeline

app.MapControllers();
app.Run();
