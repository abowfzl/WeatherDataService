using DigipayTask.Config;
using DigipayTask.Data;
using DigipayTask.Service;
using Microsoft.EntityFrameworkCore;

namespace DigipayTask.DependencyInjection;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WeatherAPIConfig>(configuration.GetSection("WeatherAPI"));

        services.AddScoped<WeatherService>();

        services.AddHttpClient<WeatherService>(client =>
        {
            client.BaseAddress = new Uri(configuration["WeatherAPI:BaseUrl"] ?? string.Empty);
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        services.AddDbContext<WeatherDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
