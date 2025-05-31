using DigipayTask.Service;
using Microsoft.AspNetCore.Mvc;

namespace DigipayTask.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController(WeatherService weatherService) : ControllerBase
{
    private readonly WeatherService _weatherService = weatherService;

    [HttpGet("get")]
    public async Task<IActionResult> GetWeather(CancellationToken cancellationToken)
    {
        var weatherJson = await _weatherService.GetLatestWeatherAsync(cancellationToken);

        if (weatherJson == null)
            return Ok(null);

        return Content(weatherJson, "application/json");
    }
}
