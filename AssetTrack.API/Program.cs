using AssetTrack.API.Data;
using AssetTrack.API.Repositories;
using AssetTrack.API.Repository; // We only need the plural namespace
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Add Controllers
builder.Services.AddControllers();

// 3. DEPENDENCY INJECTION REGISTRATION
// This line creates the map: Interface -> Class
builder.Services.AddScoped<IAssetsRepository, AssetsRepository>();

// 4. Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Pipeline Setup
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();