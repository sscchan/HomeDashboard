using System.ComponentModel.DataAnnotations;

namespace HomeDashboard.WebApplication.Dtos;

public class GetWeatherForecastsResponseDto
{
    [Required]
    public DateTime DateTime { get; set; }
    
    [Required]
    public string DeicticTime { get; set; }
    
    [Required]
    public string WeatherDescription { get; set; }
    
    [Required]
    public float RainProbabilityPercentage { get; set; }
    
    [Required]
    public float MinimumRainfall { get; set; }

    [Required]
    public float MaximumRainfall { get; set; }
    
    [Required]
    public float MinimumTemperature { get; set; }
    
    [Required]
    public float MaximumTemperature { get; set; }

    public GetWeatherForecastsResponseDto(DateTime dateTime, string deicticTime, string weatherDescription, float rainProbabilityPercentage, float minimumRainfall, float maximumRainfall, float minimumTemperature, float maximumTemperature)
    {
        DateTime = dateTime;
        DeicticTime = deicticTime;
        WeatherDescription = weatherDescription;
        RainProbabilityPercentage = rainProbabilityPercentage;
        MinimumRainfall = minimumRainfall;
        MaximumRainfall = maximumRainfall;
        MinimumTemperature = minimumTemperature;
        MaximumTemperature = maximumTemperature;
    }
}