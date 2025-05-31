# Weather Data Service

A reliable ASP.NET Core Web API service that fetches weather data from an external API, stores it locally in SQL Server, and serves cached or fresh data with fault tolerance. Designed for autonomous long-term operation in noisy network environments, ensuring continuous availability and quick responses even when the external API is temporarily unreachable.

---

## Features

- Fetches hourly temperature data from Open-Meteo API
- Caches weather data in SQL Server for offline and fallback use
- Handles noisy and unreliable network connections gracefully
- Returns fresh data within 5 seconds or falls back to last cached data
- Simple architecture following KISS principle
- Suitable for deployment in isolated or constrained environments
- Automatically applies database migrations on application startup
---

## Getting Started

### Prerequisites

- .NET 8 SDK or later
- SQL Server instance (local or remote)

### Setup Steps

1. Clone the repository  
2. Configure `appsettings.json` with your SQL Server connection string and weather API coordinates  
4. Run the project (`dotnet run`)  

---

## API Usage

**Endpoint:** `GET /api/weather/get`

- Returns weather data JSON  
- Fetches fresh data if possible  
- Falls back to cached data if fresh fetch fails  
- Returns `null` if no cached data and fetch fails  

---

## Configuration

Configure API parameters in `appsettings.json` under `"WeatherAPI"`:

```json
{
  "WeatherAPI": {
    "BaseUrl": "https://api.open-meteo.com/v1/forecast",
    "Latitude": 52.52,
    "Longitude": 13.41,
    "RequestTimeoutInSeconds": 5
  }
}
```
## Technologies
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- HttpClient Factory
- Options pattern

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For questions or feedback, please reach out to [abowfzl@gmail.com](mailto:abowfzl@gmail.com).
