using UPB.BusinessLogic.Managers;
using Serilog;
using NewAppVS.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile(
    "appsettings." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + ".json"
    )
    .Build();

// Preparar el logger para enviar logs
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Ejemplo de log
Log.Information("Initializing the server!");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<StudentManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandlerMiddleware();
// app.UseHttpsRedirection();
// app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();
