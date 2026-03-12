using AssetTrack.API.Data;
using AssetTrack.API.Repository;
using AssetTrack.API.Services; 
using Microsoft.EntityFrameworkCore;
// Hosting & Configuration - This file using the Generic Host pattern
/*
 * 1. Sets up the Kestral (Internal web server)
 * 2. Loads the configurations (appsettigns.json + Environment variables)
 * 3. Sets up Logging
 */
var builder = WebApplication.CreateBuilder(args); // Do the heavy lifting

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 32));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        serverVersion
    ));

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build(); 

// Loading Environment 
if (app.Environment.IsDevelopment()) 
{
    // Automatic Migration for Development
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // This applies any pending migrations and creates the DB if it doesn't exist
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
    
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