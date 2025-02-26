using HomeDashboard.Application.Interfaces;
using HomeDashboard.WebApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeDashboard.WebApplication.Controllers;

[ApiController]
[Route("api/Weather")]
public class WeatherController
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }
    
    /// <summary>
    /// Retrieves daily weather forecasts
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<GetWeatherForecastsResponseDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<IList<GetWeatherForecastsResponseDto>> Index()
    {
        var weatherForecasts = await _weatherForecastService.GetForecast();

        return weatherForecasts
            .Select(wf =>
            {
                var deicticTime = (wf.DateTime.DayOfWeek).ToString();
                
                if (DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)).Equals(DateOnly.FromDateTime(wf.DateTime)))
                {
                    deicticTime = "Yesterday";
                }
                else if (DateOnly.FromDateTime(DateTime.UtcNow).Equals(DateOnly.FromDateTime(wf.DateTime)))
                {
                    deicticTime = "Rest of Today";
                }
                else if (DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)).Equals(DateOnly.FromDateTime(wf.DateTime)))
                {
                    deicticTime = "Tomorrow";
                }
                    
                return new GetWeatherForecastsResponseDto(
                    wf.DateTime, deicticTime, 
                    wf.WeatherDescription, 
                    wf.RainProbabilityPercentage, wf.MinimumRainfall, wf.MaximumRainfall,
                    wf.MinimumTemperature, wf.MaximumTemperature);
            })
            .ToList();
    }
}