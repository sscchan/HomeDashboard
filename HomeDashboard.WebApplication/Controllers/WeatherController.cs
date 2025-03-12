using HomeDashboard.Application.Interfaces;
using HomeDashboard.WebApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeDashboard.WebApplication.Controllers;

[ApiController]
[Route("api/Weather")]
public class WeatherController
{
    private readonly IWeatherObservationService _weatherObservationService;
    private readonly IWeatherForecastService _weatherForecastService;
    
    public WeatherController(IWeatherObservationService weatherObservationService, IWeatherForecastService weatherForecastService)
    {
        _weatherObservationService = weatherObservationService;
        _weatherForecastService = weatherForecastService;
    }
    
    /// <summary>
    /// Retrieves daily weather forecasts
    /// </summary>
    [HttpGet]
    [Route("Observations/Latest")]
    [ProducesResponseType(typeof(GetWeatherObservationResponseDto), 200)]
    [ProducesResponseType(500)]
    public async Task<GetWeatherObservationResponseDto> GetWeatherObservation()
    {
        var weatherObservation = await _weatherObservationService.GetWeatherObservation();
        return new GetWeatherObservationResponseDto(weatherObservation.ObservationDate,
            weatherObservation.DryBulbTemperature, weatherObservation.ApparentTemperature);
    }
    
    /// <summary>
    /// Retrieves daily weather forecasts
    /// </summary>
    [HttpGet]
    [Route("Forecasts")]
    [ProducesResponseType(typeof(IList<GetWeatherForecastsResponseDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<IList<GetWeatherForecastsResponseDto>> GetWeatherForecasts()
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
                    deicticTime = "Today";
                }
                else if (DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)).Equals(DateOnly.FromDateTime(wf.DateTime)))
                {
                    deicticTime = "Tomorrow";
                }
                    
                return new GetWeatherForecastsResponseDto(
                    wf.DateTime, deicticTime, 
                    wf.WeatherIconName,
                    wf.WeatherDescription, 
                    wf.RainProbabilityPercentage, wf.MinimumRainfall, wf.MaximumRainfall,
                    wf.MinimumTemperature, wf.MaximumTemperature);
            })
            .Where(wf => wf.DeicticTime != "Yesterday")
            .ToList();
    }
}