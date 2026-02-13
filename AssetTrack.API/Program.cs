using AssetTrack.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer(); // <--- Add this
builder.Services.AddSwaggerGen();           // <--- Add this

var app = builder.Build();

// 2. Enable the Swagger UI
if (app.Environment.IsDevelopment()) // (Optional: only run in dev mode)
{
    app.UseSwagger();               // <--- Generates the JSON file
    app.UseSwaggerUI();             // <--- Generates the web page
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();