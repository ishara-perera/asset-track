using AssetTrack.API.Data;
using AssetTrack.API.Repositories;
using AssetTrack.API.Repository;
using AssetTrack.API.Services; // We only need the plural namespace
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddControllers();


builder.Services.AddScoped<IAssetsRepository, AssetsRepository>();

builder.Services.AddScoped<IAssetsService, AssetsService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Service Registration (ADD THIS LINE 👇)
builder.Services.AddScoped<IAssetsService, AssetsService>();if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();