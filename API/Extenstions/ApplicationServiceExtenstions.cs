using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Extenstions;

public static class ApplicationServiceExtenstions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration config)
    {
        // Add services to the container.
        services.AddControllers();

        services.AddCors();
        
        services.AddScoped<ITokenService , TokenService>();
        services.AddDbContext<DataContext>(opt => 
        {
            //asks: "Hey, what's the value for DefaultConnection?"
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            //It finds the answer in appsettings.json
        });

        services.AddScoped<IUserRepository , UserRepository>();
        services.AddScoped<IPhotoService , PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
    return services;
    }
}