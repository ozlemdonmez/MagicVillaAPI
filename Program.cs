using MagicVilla_WebAPI;
using MagicVilla_WebAPI.Data;
using MagicVilla_WebAPI.Logging;
using MagicVilla_WebAPI.Repository;
using MagicVilla_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logger config: SinkFile and Serilog
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add service for Logging class
// singleton means one object for this app.
builder.Services.AddSingleton<ILogging, Logging>();

// add service for sql
builder.Services.AddDbContext<ApplicationDbContext>(option
    => option.UseNpgsql(builder.Configuration.GetConnectionString("MagicVilla")));

// add automapper
builder.Services.AddAutoMapper(typeof(MapConfig));

builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();