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
            .Select(wf => new GetWeatherForecastsResponseDto(wf.Day, wf.WeatherDescription, wf.RainDescription, wf.MinimumTemperature, wf.MaximumTemperature))
            .ToList();
    }
}