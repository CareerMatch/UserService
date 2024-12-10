using CareerMatch.UserServices.Data;
using CareerMatch.UserServices.Repositories;
using CareerMatch.UserServices.Services;
using Microsoft.EntityFrameworkCore;

namespace CareerMatch.UserServices;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                               ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
//check if CI Works
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

        app.UseAuthorization(); // Make sure to keep authorization middleware if needed

        app.MapControllers();

        app.Run();
    }
}
