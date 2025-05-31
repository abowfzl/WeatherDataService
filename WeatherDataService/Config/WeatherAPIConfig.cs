namespace DigipayTask.Config;

public class WeatherAPIConfig
{
    public required string BaseUrl { get; set; }
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }
    public required int RequestTimeoutInSeconds { get; set; }
}
