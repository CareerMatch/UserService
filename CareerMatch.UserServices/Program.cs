using CareerMatch.UserServices.Data;
using CareerMatch.UserServices.Repositories;
using CareerMatch.UserServices.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register services and repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserReadService, UserReadService>(); // Register read service
builder.Services.AddScoped<IUserWriteService, UserWriteService>(); // Register write service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();