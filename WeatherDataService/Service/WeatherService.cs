using DigipayTask.Config;
using DigipayTask.Data;
using DigipayTask.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DigipayTask.Service;

public class WeatherService(HttpClient httpClient, WeatherDbContext dbContext, IOptions<WeatherAPIConfig> options)
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly WeatherDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly WeatherAPIConfig _config = options?.Value ?? throw new ArgumentNullException(nameof(options));

    public async Task<string?> GetLatestWeatherAsync(CancellationToken cancellationToken)
    {
        string? weatherData = await TryFetchWeatherFromApiAsync(cancellationToken);

        if (!string.IsNullOrEmpty(weatherData))
        {
            await SaveWeatherDataAsync(weatherData, cancellationToken);
            return weatherData;
        }

        return await GetLastStoredWeatherDataAsync(cancellationToken);
    }

    private async Task<string?> TryFetchWeatherFromApiAsync(CancellationToken cancellationToken)
    {
        var requestUrl = BuildRequestUrl();

        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        linkedCts.CancelAfter(TimeSpan.FromSeconds(_config.RequestTimeoutInSeconds));

        try
        {
            var response = await _httpClient.GetAsync(requestUrl, linkedCts.Token);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(linkedCts.Token);
            }
        }
        catch
        {
            Console.WriteLine("Error fetching weather data from API. Falling back to stored data.");
        }

        return null;
    }

    private string BuildRequestUrl()
    {
        return $"?latitude={_config.Latitude}&longitude={_config.Longitude}&hourly=temperature_2m";
    }

    private async Task SaveWeatherDataAsync(string jsonData, CancellationToken cancellationToken)
    {
        var weatherInfo = new WeatherInfo
        {
            CreatedDate = DateTime.UtcNow,
            JsonRawData = jsonData
        };

        _dbContext.WeatherInfos.Add(weatherInfo);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<string?> GetLastStoredWeatherDataAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.WeatherInfos
            .OrderByDescending(w => w.CreatedDate)
            .Select(w => w.JsonRawData)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

